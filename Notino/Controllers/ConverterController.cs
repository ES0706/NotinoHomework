using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notino.Application.ConvertFile;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Controllers
{
    [Route("api/converter")]
    public class ConverterController : BaseController
    {
        public ConverterController(
        IMediator mediator,
        ILogger<ConverterController> logger
        // SmtpClient smtpClient
        ) : base(mediator,
        logger
        // smtpClient
        )
        { }

        [HttpPost("xml-to-json")]
        public async Task<ActionResult> ConvertXmlToJson(ConvertXmlToJsonQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            if (result.Failed)
                return BadRequest(result.Errors);

            // TODO: save to file system and send url in mail
            // await _smtpClient.SendMailAsync(new MailMessage(
            //     to: "test",
            //     from: "test@test.com",
            //     subject: "Converted File",
            //     body: "Url: test"
            // ));

            return Ok(result.Data);
        }
    }
}
