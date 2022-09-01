using Echo.Api.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Echo.Api.Tests.Controllers;

public class EchoControllerTests
{
    // make sure that constructor throws an exeption if logger is null
    [Fact]
    public void Constructor_ThrowsException_IfLoggerIsNull()
    {
        // arrange
        ILogger<EchoController> logger = null;
        // create mocked appnameprovider
        var appNameProvider = new Mock<IAppNameProvider>();
        // act
        Action action = () => new EchoController(logger, appNameProvider.Object );

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }
    
    // GetMessage returns response with given message
    [Fact]
    public async Task GetMessage_ReturnsResponseWithGivenMessage()
    {
        // arrange
        var logger = new Mock<ILogger<EchoController>>();
        var appNameProvider = new Mock<IAppNameProvider>();
        // setup appnameprovider mock to return a value
        appNameProvider.Setup(x => x.GetAppName()).Returns("unit test");

        var controller = new EchoController(logger.Object, appNameProvider.Object);
        var message = "test message";
        var expectedMessage = "test message unit test";
        // act
        var response = await controller.GetMessage(message, CancellationToken.None);
        // assert
        //assert that response.message contains expectedmessage
        Assert.Contains(expectedMessage, response.Message);
    }

    // constructor throws an exception if appnameprovider is null
    [Fact] 
    public void Constructor_ThrowsException_IfAppNameProviderIsNull()
    {
        // arrange
        var logger = new Mock<ILogger<EchoController>>();
        IAppNameProvider appNameProvider = null;
        // act
        Action action = () => new EchoController(logger.Object, appNameProvider);
        // assert
        Assert.Throws<ArgumentNullException>(action);
    }
}