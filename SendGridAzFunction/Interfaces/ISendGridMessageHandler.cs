using SendGrid.Helpers.Mail;

namespace SendGridAzFunction.Interfaces;

public interface ISendGridMessageHandler
{
    SendGridMessage MakeSendGridMessage(ContactForm contact);
}
