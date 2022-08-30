using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Echo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {
        private readonly ILogger<EchoController> _logger;
        private readonly IAppNameProvider _appNameProvider;

        public EchoController(ILogger<EchoController> logger, IAppNameProvider appNameProvider)
        {
            _logger = logger;
            _appNameProvider = appNameProvider.GetAppName();
        }


        [HttpGet]
        public Task<Response> Post(string message, CancellationToken token)
        {
            _logger.LogInformation($"Received message: {message}");

            var from  = _appNameProvider.GetAppName() is null ? string.Empty : _appNameProvider.GetAppName();
            
            return Task.FromResult(new Response
                {
                    Message = $"{message} {from}"
                }
            );
        }
    }
}