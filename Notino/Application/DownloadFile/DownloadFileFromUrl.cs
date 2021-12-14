using MediatR;
using Notino.Contracts;
using Notino.Readers;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Application.DownloadFile
{
    public record DownloadFileFromUrlQuery(string Url) : IRequest<Result<DownloadFileFromUrlResponse>>;
    public record DownloadFileFromUrlResponse(string Path, byte[] Bytes);
    public class DownloadFileFromUrlHandler : IRequestHandler<DownloadFileFromUrlQuery, Result<DownloadFileFromUrlResponse>>
    {
        private readonly ReaderProviderFactory _readerProviderFactory;

        public DownloadFileFromUrlHandler(ReaderProviderFactory readerProviderFactory)
        {
            _readerProviderFactory = readerProviderFactory;
        }

        public async Task<Result<DownloadFileFromUrlResponse>> Handle(DownloadFileFromUrlQuery request, CancellationToken cancellationToken)
        {
            var readerProvider = _readerProviderFactory.GetReaderProvider(ReaderSourceType.Url);
            var result = await readerProvider.Read(request.Url);

            if (result.Failed)
                return Result<DownloadFileFromUrlResponse>.Failure(result.Errors);

            return Result<DownloadFileFromUrlResponse>.Success(new DownloadFileFromUrlResponse(request.Url, result.Data));
        }
    }
}
