using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.Interfaces;

public interface ISendGridMessageHandler
{
    SendGridMessage MakeSendGridMessage(ContactForm contact);
}
