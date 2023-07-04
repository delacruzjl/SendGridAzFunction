namespace Jodelac.SendGridAzFunction.Constants;

public static class SendGridConstants
{
    public static string SENDGRID_API_KEY => "AzureWebJobsSendGridApiKey";
    public static string SENDGRID_NEWSLETTER_LIST_ID => "SendGrid:NewsletterListId";
    public static string SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID => "SendGrid:TemplateId";
    public static string SENDGRID_SENDER_EMAIL_ADDRESS => "SendGrid:EmailAddress";
    public static string SENDGRID_SENDER_NAME => "SendGrid:SenderName";
    public static string SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT => "SendGrid:SubjectLine";
    public static string SENDGRID_SUPRESSION_GROUP_ID => "SendGrid:SuppressionGroupId";
    public static string WEBSITE_ADMIN_EMAIL => "SendGrid:WebsiteAdminEmail";

}
