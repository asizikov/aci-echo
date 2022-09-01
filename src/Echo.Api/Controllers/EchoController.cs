using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Echo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EchoController : ControllerBase
{
    private readonly ILogger<EchoController> _logger;
    private readonly IAppNameProvider _appNameProvider;

    public EchoController(ILogger<EchoController> logger, IAppNameProvider appNameProvider)
    {
        // throw exception when parameters are null
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _appNameProvider = appNameProvider ?? throw new ArgumentNullException(nameof(appNameProvider));
    }
    
    [HttpGet]
    public Task<Response> GetMessage(string message, CancellationToken token)
    {
        _logger.LogInformation("Received message: {Message}", message);

        var from = _appNameProvider.GetAppName() is null ? string.Empty : _appNameProvider.GetAppName();

        return Task.FromResult(new Response
            {
                Message = $"{message} {from}"
            }
        );
    }
}