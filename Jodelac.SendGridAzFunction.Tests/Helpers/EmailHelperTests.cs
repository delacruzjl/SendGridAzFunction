using System.Text;
using System.Text.Json;
using DataGenerator;
using DataGenerator.Sources;
using Jodelac.SendGridAzFunction.Interfaces;
using Jodelac.SendGridAzFunction.Models;
using Jodelac.SendGridAzFunction.Validators;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.FunctionHelpers.Tests
{
    public class EmailHelperTests
    {
        public EmailHelperTests()
        {
            Generator.Default.Configure(c =>
              c.Entity<ContactForm>(f =>
              {
                  f.Property(p => p.FullName).DataSource<NameSource>();
                  f.Property(p => p.Content).DataSource<LoremIpsumSource>();
              }));
        }

        [Fact]
        public async Task QueueEmailToSendGrid_WithValidContactForm_ReturnsValidSendGridMessage()
        {
            // Arrange
            var contactForm = Generator.Default.Single<ContactForm>();

            JsonSerializerOptions jsonOptions = new();
            ContactFormValidator validator = new();
            Mock<ISendGridContactHandler> sendGridContactHandlerMock = new();
            Mock<IAsyncCollector<SendGridMessage>> messageCollectorMock = new();
            NullLogger<EmailHelper> logger = new();
            Mock<ISendGridMessageHandler> sendGridMessageHandlerStub = new();
            var contact = JsonSerializer.Serialize(contactForm, jsonOptions);
            MemoryStream bodyFake = new(Encoding.UTF8.GetBytes(contact));

            var _sut = new EmailHelper(jsonOptions, validator, sendGridMessageHandlerStub.Object, sendGridContactHandlerMock.Object);

            // Act
            await _sut.QueueEmailToSendGrid(bodyFake, messageCollectorMock.Object, logger);

            // Assert
            _ = sendGridMessageHandlerStub
                .Setup(_ => _.MakeSendGridMessage(It.IsAny<ContactForm>()));

            messageCollectorMock.Verify(m => m.AddAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()), Times.Once());
        }

    }
}
