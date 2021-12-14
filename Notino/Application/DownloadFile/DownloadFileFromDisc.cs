using MediatR;
using Notino.Contracts;
using Notino.Readers;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Application.DownloadFile
{
    public record DownloadFileFromDiscQuery(string Path) : IRequest<Result<DownloadFileFromDiscResponse>>;
    public record DownloadFileFromDiscResponse(string Path, byte[] Bytes);

    public class DownloadFileFromDiscHandler : IRequestHandler<DownloadFileFromDiscQuery, Result<DownloadFileFromDiscResponse>>
    {
        private readonly ReaderProviderFactory _readerProviderFactory;

        public DownloadFileFromDiscHandler(ReaderProviderFactory readerProviderFactory)
        {
            _readerProviderFactory = readerProviderFactory;
        }

        public async Task<Result<DownloadFileFromDiscResponse>> Handle(DownloadFileFromDiscQuery request, CancellationToken cancellationToken)
        {
            var readerProvider = _readerProviderFactory.GetReaderProvider(ReaderSourceType.File);
            var result = await readerProvider.Read(request.Path);

            if (result.Failed)
                return Result<DownloadFileFromDiscResponse>.Failure(result.Errors);

            return Result<DownloadFileFromDiscResponse>.Success(new DownloadFileFromDiscResponse(request.Path, result.Data));
        }
    }
}
