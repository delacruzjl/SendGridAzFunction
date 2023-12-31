﻿using System.Net;

namespace Jodelac.SendGridAzFunctionTests.Handlers;

public class SendGridContactHandlerTests
{
    private readonly Mock<ISendGridClient> _sendGridClientMock;
    private readonly Mock<ILogger<SendGridContactHandler>> _loggerMock;
    private readonly IOptionsMonitor<SendGridConfiguration> _sendGridConfiguration;
    private readonly JsonSerializerOptions _jsonOptions;

    public SendGridContactHandlerTests()
    {
        _sendGridClientMock = new();
        _loggerMock = new();
        _sendGridConfiguration = Mock.Of<IOptionsMonitor<SendGridConfiguration>>(x =>
            x.CurrentValue == Generator.Default.Single<SendGridConfiguration>());
        _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }


    [Fact]
    public async Task AddContactToSendGridGroup_ReturnsExpectedStatusCode()
    {
        // Arrange
        var contact = Generator.Default.Single<NewsletterContact>();

        _sendGridClientMock
            .Setup(x =>
                x.RequestAsync(It.IsAny<BaseClient.Method>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response(HttpStatusCode.OK, new StringContent("test"), null));

        var handler = new SendGridContactHandler(_sendGridConfiguration, _sendGridClientMock.Object, _jsonOptions);

        // Act
        var result = await handler.AddContactToSendGridGroup(contact, _loggerMock.Object);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result);
    }

    [Fact]
    public async Task AddContactToSendGridGroup_ThrowsHttpRequestException_ForErrorResponse()
    {
        // Arrange
        var contact = Generator.Default.Single<NewsletterContact>();

        _sendGridClientMock
            .Setup(x =>
                x.RequestAsync(It.IsAny<SendGridClient.Method>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response(HttpStatusCode.BadRequest, new StringContent("Error Message"), null));

        var handler = new SendGridContactHandler(_sendGridConfiguration, _sendGridClientMock.Object, _jsonOptions);

        // Act and Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => handler.AddContactToSendGridGroup(contact, _loggerMock.Object));
    }

    [Fact]
    public async Task AddContactToSendGridList_ReturnsExpectedStatusCode()
    {
        // Arrange
        var contact = Generator.Default.Single<NewsletterContact>();

        _sendGridClientMock
            .Setup(x =>
                x.RequestAsync(It.IsAny<SendGridClient.Method>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response(HttpStatusCode.OK, new StringContent("test"), null));

        var handler = new SendGridContactHandler(_sendGridConfiguration, _sendGridClientMock.Object, _jsonOptions);

        // Act
        var result = await handler.AddContactToSendGridList(contact, _loggerMock.Object);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result);
    }

    [Fact]
    public async Task AddContactToSendGridList_ThrowsHttpRequestException_ForErrorResponse()
    {
        // Arrange
        var contact = Generator.Default.Single<NewsletterContact>();

        _sendGridClientMock
            .Setup(x =>
                x.RequestAsync(It.IsAny<SendGridClient.Method>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response(HttpStatusCode.BadRequest, new StringContent("test"), null));

        var handler = new SendGridContactHandler(_sendGridConfiguration, _sendGridClientMock.Object, _jsonOptions);

        // Act and Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => handler.AddContactToSendGridList(contact, _loggerMock.Object));
    }
}
