using Notino.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Notino.Readers
{
    public class FileReaderProvider : IReaderProvider
    {
        public ReaderSourceType SourceType => ReaderSourceType.File;

        public async Task<Result<byte[]>> Read(string sourcePath)
        {
            try
            {
                var result = await File.ReadAllBytesAsync(sourcePath);
                return Result<byte[]>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<byte[]>.Failure(ex.Message);
            }
        }
    }
}
