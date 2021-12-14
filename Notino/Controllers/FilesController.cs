using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notino.Application.DownloadFile;
using Notino.Application.UploadFile;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Controllers
{
    [Route("api/files")]
    public class FilesController : BaseController
    {
        public FilesController(IMediator mediator, ILogger<FilesController> logger) : base(mediator, logger) { }

        [HttpPost("upload-file")]
        public async Task<ActionResult> UploadFileToDisc(IFormFile file, CancellationToken cancellationToken)
        {
            var command = new UploadFileToDiscCommand(file);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.Failed)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost("download-file")]
        public async Task<ActionResult> DownloadFileFromDisc(DownloadFileFromDiscQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            if (result.Failed)
                return BadRequest(result.Errors);

            return File(result.Data.Bytes, "text/plain", Path.GetFileName(result.Data.Path));
        }

        [HttpPost("download-file-from-url")]
        public async Task<ActionResult> DownloadFileFromUrl(DownloadFileFromUrlQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            if (result.Failed)
                return BadRequest(result.Errors);

            return File(result.Data.Bytes, "text/plain", Path.GetFileName((new Uri(result.Data.Path)).LocalPath));
        }
    }
}
