using MediatR;
using Microsoft.AspNetCore.Http;
using Notino.Contracts;
using Notino.Writers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Application.UploadFile
{
    public record UploadFileToDiscResponse(string Path);
    public record UploadFileToDiscCommand(IFormFile File) : IRequest<Result<UploadFileToDiscResponse>>;
    public class UploadFileToDiscHandler : IRequestHandler<UploadFileToDiscCommand, Result<UploadFileToDiscResponse>>
    {
        private readonly WriterProviderFactory _writerProviderFactory;

        public UploadFileToDiscHandler(WriterProviderFactory writerProviderFactory)
        {
            _writerProviderFactory = writerProviderFactory;
        }

        public async Task<Result<UploadFileToDiscResponse>> Handle(UploadFileToDiscCommand request, CancellationToken cancellationToken)
        {
            var writerProvider = _writerProviderFactory.GetWriterProvider(WriterTargetType.File);
            string content;

            try
            {
                using var reader = new StreamReader(request.File.OpenReadStream());
                content = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                return Result<UploadFileToDiscResponse>.Failure(ex.Message);
            }

            var path = $"Files/{request.File.FileName}";
            var result = await writerProvider.Write(path, content);

            if (result.Failed)
                return Result<UploadFileToDiscResponse>.Failure(result.Errors);

            return Result<UploadFileToDiscResponse>.Success(new UploadFileToDiscResponse(path));
        }
    }
}
