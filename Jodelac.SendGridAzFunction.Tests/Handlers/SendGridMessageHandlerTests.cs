using DataGenerator.Sources;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.Tests.Handlers
{
    public class SendGridMessageHandlerTests
    {
        private readonly SendGridMessageHandler _sut;
        private readonly string ExpectedEmail;
        private readonly string ExpectedSubject;

        public SendGridMessageHandlerTests()
        {
            Generator.Default.Configure(c =>
             c.Entity<SendGridConfiguration>(f =>
             {
                 f.Property(p => p.TemplateId).DataSource<GuidSource>();
                 f.Property(p => p.SenderName).DataSource<NameSource>();
                 f.Property(p => p.WebsiteAdminEmail).DataSource<EmailSource>();
             }));
            Generator.Default.Configure(c =>
             c.Entity<ContactForm>(f =>
             {
                 f.Property(p => p.FullName).DataSource<NameSource>();
                 f.Property(p => p.Content).DataSource<LoremIpsumSource>();
             }));

            var sendGridConfig = Generator.Default.Single<SendGridConfiguration>();
            var config = Mock.Of<IOptionsMonitor<SendGridConfiguration>>(x =>
                x.CurrentValue == sendGridConfig);

            ExpectedEmail = sendGridConfig.WebsiteAdminEmail;
            ExpectedSubject = sendGridConfig.SubjectLine;

            _sut = new(config);
        }

        [Fact]
        public void MakeSendGridMessage_ValidContactForm()
        {
            // Arrange
            var contactForm = Generator.Default.Single<ContactForm>();

            // Act
            var result = _sut.MakeSendGridMessage(contactForm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ExpectedSubject, result.Subject);
            Assert.Collection(result.Personalizations,
                personalization =>
                {
                    Assert.Single(personalization.Tos);
                    Assert.Equal(new EmailAddress(ExpectedEmail), personalization.Tos[0]);
                });
        }
    }
}
