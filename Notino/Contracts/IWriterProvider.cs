using System.Threading.Tasks;

namespace Notino.Contracts
{
    public interface IWriterProvider
    {
        public Task<Result> Write(string targetPath, string content);
        public WriterTargetType TargetType { get; }
    }

    public enum WriterTargetType
    {
        File
    }
}
