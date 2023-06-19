namespace Jodelac.SendGridAzFunction.Models;

public class SendGridConfiguration : ApiModelBase
{
    public const string SENDGRID_API_KEY = "AzureWebJobsSendGridApiKey";
    public const string SENDGRID_NEWSLETTER_LIST_ID = "SendGrid:NewsletterListId";
    public const string SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID = "SendGrid:TemplateId";
    public const string SENDGRID_SENDER_EMAIL_ADDRESS = "SendGrid:EmailAddress";
    public const string SENDGRID_SENDER_NAME = "SendGrid:SenderName";
    public const string SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT = "SendGrid:SubjectLine";
    public const string SENDGRID_SUPRESSION_GROUP_ID = "SendGrid:SuppressionGroupId";
    public const string WEBSITE_ADMIN_EMAIL = "SendGrid:WebsiteAdminEmail";

    public SendGridConfiguration()
    {
        NewsletterListId = string.Empty;
        TemplateId = string.Empty;
        EmailAddress = string.Empty;
        SenderName = string.Empty;
        SubjectLine = string.Empty;
        WebsiteAdminEmail = string.Empty;
        SuppressionGroupId = 0;
    }


    public string NewsletterListId { get; set; }
    public string TemplateId { get; set; }

    public string EmailAddress { get; set; }

    public string SenderName { get; set; }

    public string SubjectLine { get; set; }
    public string WebsiteAdminEmail { get; set; }
    public int SuppressionGroupId { get; set; }
}