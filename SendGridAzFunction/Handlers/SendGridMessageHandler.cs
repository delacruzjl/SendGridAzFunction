using Jodelac.SendGridAzFunction.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.Handlers;
public class SendGridMessageHandler : ISendGridMessageHandler
{
    private readonly SendGridConfiguration _sendGridConfiguration;

    public SendGridMessageHandler(
        IOptionsMonitor<SendGridConfiguration> sendGridConfiguration)
    {
        _sendGridConfiguration = sendGridConfiguration.CurrentValue;
    }

    public SendGridMessage MakeSendGridMessage(ContactForm contact)
    {

        SendGridMessage msg = new();
        msg.SetTemplateId(_sendGridConfiguration.TemplateId);
        msg.SetFrom(_sendGridConfiguration.EmailAddress, _sendGridConfiguration.SenderName);
        msg.AddTo(_sendGridConfiguration.WebsiteAdminEmail);

        msg.Subject = _sendGridConfiguration.SubjectLine;
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