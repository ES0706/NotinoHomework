using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;

namespace Notino.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase, IDisposable
    {
        protected IMediator _mediator;
        protected ILogger _logger;
        protected SmtpClient _smtpClient;
        protected BaseController(IMediator mediator, ILogger logger, SmtpClient smtpClient = null)
        {
            _mediator = mediator;
            _logger = logger;
            if (smtpClient != null)
                _smtpClient = smtpClient;
        }

        public void Dispose()
        {
            if (_smtpClient != null)
                _smtpClient.Dispose();
        }
    }
}
