using System.Text;
using System.Text.Json;
using FluentValidation;
using Jodelac.SendGridAzFunction.FunctionHelpers;
using Jodelac.SendGridAzFunction.Interfaces;
using Jodelac.SendGridAzFunction.Models;
using Microsoft.Extensions.Logging;
using Moq;

public class SubscriptionHelperTests
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<ISendGridContactHandler> _sendgridContactHandlerStub;
    private readonly SubscriptionHelper _sut;

    private JsonSerializerOptions _jsonOptions;

    public SubscriptionHelperTests()
    {
        _loggerMock = new Mock<ILogger>();
        _sendgridContactHandlerStub = new Mock<ISendGridContactHandler>();
        _sut = new SubscriptionHelper(_jsonOptions, new InlineValidator<NewsletterContact>(), _sendgridContactHandlerStub.Object);
    }

    [Fact]
    public async Task SubscribeContactToSite_WithValidInput_Success()
    {
        // Arrange        
        var contact = new NewsletterContact { FirstName = "Test", LastName = "Name", Email = "test@test.com" };
        var json = JsonSerializer.Serialize(contact);
        var body = new MemoryStream(Encoding.UTF8.GetBytes(json));

        _sendgridContactHandlerStub.Setup(x => x.AddContactToSendGridList(contact, _loggerMock.Object)).ReturnsAsync(200);
        _sendgridContactHandlerStub.Setup(x => x.AddContactToSendGridGroup(contact, _loggerMock.Object)).ReturnsAsync(200);

        // Act
        var response = await _sut.SubscribeContactToSite(body, _loggerMock.Object, 0);

        // Assert
        response.Equals(200);
        _sendgridContactHandlerStub.Verify(x => x.AddContactToSendGridList(contact, _loggerMock.Object), Times.Once);
        _sendgridContactHandlerStub.Verify(x => x.AddContactToSendGridGroup(contact, _loggerMock.Object), Times.Once);
    }

    [Fact]
    public async Task SubscribeContactToSite_WithInvalidInput_ThrowsException()
    {
        // Arrange
        var body = new MemoryStream();
        var contact = new NewsletterContact { Email = "test.com" };
        var json = JsonSerializer.Serialize(contact);
        var writer = new StreamWriter(body);
        writer.Write(json);
        writer.Flush();
        body.Position = 0;

        _sendgridContactHandlerStub.Setup(x => x.AddContactToSendGridList(It.IsAny<NewsletterContact>(), It.IsAny<ILogger>())).ReturnsAsync(400);
        _sendgridContactHandlerStub.Setup(x => x.AddContactToSendGridGroup(It.IsAny<NewsletterContact>(), It.IsAny<ILogger>())).ReturnsAsync(400);

        // Act & Assert
        await Assert.ThrowsAsync<JsonException>(() => _sut.SubscribeContactToSite(body, _loggerMock.Object, 0));
    }
}
