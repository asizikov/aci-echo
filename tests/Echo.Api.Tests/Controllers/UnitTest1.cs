using Echo.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Echo.Api.Tests.Controllers;

public class EchoControllerTests
{
    // test that GetMessage returns the message passed in the request
    [Fact]
    public async Task GetMessage_ReturnsMessage()
    {
        // Arrange
        //create NullLogger
        var logger = new NullLogger<EchoController>();
        //create mock IAppNameProvider
        var appNameProvider = new Mock<IAppNameProvider>();
        appNameProvider.Setup(x => x.GetAppName()).Returns("Echo");
        // create controller with dependencies
        var controller = new EchoController(logger, appNameProvider.Object);
        var message = "Hello World";
        var expected = "Hello World Echo";
        // Act
        var result = await controller.GetMessage(message, CancellationToken.None);
        // Assert that result contains the message
        Assert.Equal(expected, result.Message);
        
    }
    
    //test that controller constructor throws exception if logger is null
    [Fact]
    public void Constructor_ThrowsExceptionIfLoggerIsNull()
    {
        // Arrange
        var logger = null as ILogger<EchoController>;
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => new EchoController(logger, new Mock<IAppNameProvider>().Object));
    }

    //test that controller constructor throws exception if appNameProvider is null
    [Fact]
    public void Constructor_ThrowsExceptionIfAppNameProviderIsNull()
    {
        // Arrange
        var logger = new Mock<ILogger<EchoController>>().Object;
        var appNameProvider = null as IAppNameProvider;
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => new EchoController(logger, appNameProvider));
    }
}