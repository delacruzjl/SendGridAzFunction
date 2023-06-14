using SendGrid.Helpers.Mail;
using SendGridAzFunction.Interfaces;

namespace SendGridAzFunction.Handlers;
public class SendGridMessageHandler : ISendGridMessageHandler
{
    private readonly SendGridConfiguration _sendGridConfiguration;

    public SendGridMessageHandler(SendGridConfiguration sendGridConfiguration)
    {
        _sendGridConfiguration = sendGridConfiguration;
    }

    public SendGridMessage MakeSendGridMessage(ContactForm contact)
    {

        var msg = new SendGridMessage();
        msg.SetTemplateId(_sendGridConfiguration.TemplateId);
        msg.SetFrom(_sendGridConfiguration.EmailAddress, _sendGridConfiguration.SenderName);
        msg.AddTo(_sendGridConfiguration.WebsiteAdminEmail);
        msg.SetSubject(_sendGridConfiguration.SubjectLine);

        msg.SetTemplateData(new
        {
            fullName = contact.FullName,
            email = contact.Email,
            content = contact.Content,
            subject = _sendGridConfiguration.SubjectLine
        });

        // disable tracking settings
        // ref.: https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);
        msg.SetOpenTracking(false);
        msg.SetGoogleAnalytics(false);
        msg.SetSubscriptionTracking(true);

        return msg;
    }
}