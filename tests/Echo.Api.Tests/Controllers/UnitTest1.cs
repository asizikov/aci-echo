using Echo.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Echo.Api.Tests.Controllers;

public class EchoControllerTests
{
    // TODO: Add tests for the EchoController
    [Fact]
    public async Task Echo_ReturnsEchoMessage()
    {
        // Arrange
        var logger = new Mock<ILogger<EchoController>>();
        // create a mocked AppNameProvider
        var appNameProvider = new Mock<IAppNameProvider>();
        //set the mocked AppNameProvider's GetAppName method to return "Echo"
        appNameProvider.Setup(x => x.GetAppName()).Returns("Echo");
        
        var controller = new EchoController(logger.Object, appNameProvider.Object);
        var message = "Hello World!";
        var expected = new Response { Message = "Hello World! Echo" };
        // Act
        var result = await controller.GetMessage(message, CancellationToken.None);
        // Assert
        Assert.Equal(expected.Message, result.Message);
    }
    
    // contstructor throws when logger is null
    [Fact]
    public void EchoController_Throws_WhenLoggerIsNull()
    {
        // Arrange
        var appNameProvider = new Mock<IAppNameProvider>();
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new EchoController(null, appNameProvider.Object));
    }
    
    // contstructor throws when appNameProvider is null
    [Fact]
    public void EchoController_Throws_WhenAppNameProviderIsNull()
    {
        // Arrange
        var logger = new Mock<ILogger<EchoController>>();
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new EchoController(logger.Object, null));
    }
}