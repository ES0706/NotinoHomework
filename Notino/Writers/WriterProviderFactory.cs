using Notino.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Notino.Writers
{
    public class WriterProviderFactory
    {
        private readonly IReadOnlyDictionary<WriterTargetType, IWriterProvider> _writerProviders;

        public WriterProviderFactory()
        {
            var writerProviderType = typeof(IWriterProvider);
            _writerProviders = writerProviderType.Assembly.ExportedTypes
                .Where(x => writerProviderType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x))
                .Cast<IWriterProvider>()
                .ToImmutableDictionary(x => x.TargetType, x => x);
        }

        public IWriterProvider GetWriterProvider(WriterTargetType targetType) => _writerProviders[targetType];
    }
}
