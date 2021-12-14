using Notino.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Notino.Converters
{
    public class ConverterProviderFactory
    {
        private readonly IReadOnlyDictionary<ConversionType, IConverterProvider> _converterProviders;

        public ConverterProviderFactory()
        {
            var converterProviderType = typeof(IConverterProvider);
            _converterProviders = converterProviderType.Assembly.ExportedTypes
                .Where(x => converterProviderType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x))
                .Cast<IConverterProvider>()
                .ToImmutableDictionary(x => x.ConversionType, x => x);
        }

        public IConverterProvider GetConverterProvider(ConversionType conversionType) => _converterProviders[conversionType];
    }
}
