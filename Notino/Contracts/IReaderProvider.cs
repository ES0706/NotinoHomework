using System.Threading.Tasks;

namespace Notino.Contracts
{
    public interface IReaderProvider
    {
        public Task<Result<byte[]>> Read(string sourcePath);
        public ReaderSourceType SourceType { get; }
    }

    public enum ReaderSourceType
    {
        File,
        Url
    }
}
