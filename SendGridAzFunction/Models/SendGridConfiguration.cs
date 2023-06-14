using Microsoft.Extensions.Configuration;

namespace Jodelac.SendGridAzFunction.Models;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Major Code Smell", "S3928:Parameter names used into ArgumentException constructors should match an existing one ",
    Justification = "ArgumentNullException is the best option for this situation")]
public class SendGridConfiguration
{
    public const string SENDGRID_API_KEY = "AzureWebJobsSendGridApiKey";
    private const string SENDGRID_NEWSLETTER_LIST_ID = "SENDGRID_NEWSLETTER_LIST_ID";
    private const string SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID = "SENDGRID_TEMPLATE_ID";
    private const string SENDGRID_SENDER_EMAIL_ADDRESS = "SENDGRID_EMAIL_ADDRESS";
    private const string SENDGRID_SENDER_NAME = "SENDGRID_SENDER_NAME";
    private const string SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT = "SENDGRID_SUBJECT_LINE";
    private const string SENDGRID_SUPRESSION_GROUP_ID = "SENDGRID_SUPPRESSION_GROUP_ID";
    private const string WEBSITE_ADMIN_EMAIL = "WEBSITE_ADMIN_EMAIL";

    private readonly IConfiguration _configuration;

    public SendGridConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ApiKey =>
        _configuration[SENDGRID_API_KEY]
        ?? throw new ArgumentNullException(SENDGRID_API_KEY);

    public string NewsletterListId =>
        _configuration[SENDGRID_NEWSLETTER_LIST_ID]
        ?? throw new ArgumentNullException(SENDGRID_NEWSLETTER_LIST_ID);

    public string TemplateId =>
        _configuration[SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID]
        ?? throw new ArgumentNullException(SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID);

    public string EmailAddress =>
        _configuration[SENDGRID_SENDER_EMAIL_ADDRESS]
        ?? throw new ArgumentNullException(SENDGRID_SENDER_EMAIL_ADDRESS);

    public string SenderName =>
        _configuration[SENDGRID_SENDER_NAME]
        ?? throw new ArgumentNullException(SENDGRID_SENDER_NAME);

    public string SubjectLine =>
        _configuration[SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT]
        ?? throw new ArgumentNullException(SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT);

    public string WebsiteAdminEmail =>
        _configuration[WEBSITE_ADMIN_EMAIL]
        ?? throw new ArgumentNullException(WEBSITE_ADMIN_EMAIL);

    public int SuppressionGroupId =>
        int.Parse(_configuration[SENDGRID_SUPRESSION_GROUP_ID]
        ?? throw new ArgumentNullException(SENDGRID_SUPRESSION_GROUP_ID));
}