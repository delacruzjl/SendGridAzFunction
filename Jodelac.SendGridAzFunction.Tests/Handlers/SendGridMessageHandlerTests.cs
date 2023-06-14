using Jodelac.SendGridAzFunction.Handlers;
using Jodelac.SendGridAzFunction.Models;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.Tests.Handlers
{
    public class SendGridMessageHandlerTests
    {
        private readonly SendGridMessageHandler _sut;

        public SendGridMessageHandlerTests()
        {
            var configValues = new Dictionary<string, string>
            {
                {SendGridConfiguration.SENDGRID_SENDER_EMAIL_ADDRESS, "test@example.com"},
                {SendGridConfiguration.SENDGRID_SENDER_NAME, "Tester"},
                {SendGridConfiguration.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT, "Test"},
                {SendGridConfiguration.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, "Test" },
                {SendGridConfiguration.WEBSITE_ADMIN_EMAIL, "test@example.com" }
            };

            var configurationFake = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

            SendGridConfiguration config = new(configurationFake);
            _sut = new(config);
        }

        [Fact]
        public void MakeSendGridMessage_ValidContactForm()
        {
            // Arrange
            var contactForm = new ContactForm
            {
                FullName = "John Doe",
                Email = "johndoe@example.com",
                Content = "Hello, World!"
            };

            // Act
            var result = _sut.MakeSendGridMessage(contactForm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Subject);
            Assert.Collection(result.Personalizations,
                personalization =>
                {
                    Assert.Single(personalization.Tos);
                    Assert.Equal(new EmailAddress("test@example.com"), personalization.Tos[0]);
                });
        }
    }
}
