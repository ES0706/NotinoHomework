using Notino.Contracts;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Notino.Readers
{
    public class UrlReaderProvider : IReaderProvider
    {
        public ReaderSourceType SourceType => ReaderSourceType.Url;

        public async Task<Result<byte[]>> Read(string sourcePath)
        {
            try
            {
                using var client = new WebClient();
                var result = client.DownloadData(sourcePath);
                return Result<byte[]>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<byte[]>.Failure(ex.Message);
            }
        }
    }
}
