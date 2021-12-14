using Notino.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Notino.Readers
{
    public class ReaderProviderFactory
    {
        private readonly IReadOnlyDictionary<ReaderSourceType, IReaderProvider> _readerProviders;

        public ReaderProviderFactory()
        {
            var readerProviderType = typeof(IReaderProvider);
            _readerProviders = readerProviderType.Assembly.ExportedTypes
                .Where(x => readerProviderType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x))
                .Cast<IReaderProvider>()
                .ToImmutableDictionary(x => x.SourceType, x => x);
        }

        public IReaderProvider GetReaderProvider(ReaderSourceType sourceType) => _readerProviders[sourceType];
    }
}
