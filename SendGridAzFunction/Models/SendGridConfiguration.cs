namespace Jodelac.SendGridAzFunction.Models;

public class SendGridConfiguration
{
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


    public string NewsletterListId { get; init; }
    public string TemplateId { get; init; }

    public string EmailAddress { get; init; }

    public string SenderName { get; init; }

    public string SubjectLine { get; init; }
    public string WebsiteAdminEmail { get; init; }
    public int SuppressionGroupId { get; init; }
}