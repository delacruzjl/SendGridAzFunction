using FluentValidation;

namespace Jodelac.SendGridAzFunction.Validators;

public class SendGridConfigurationValidator : AbstractValidator<SendGridConfiguration>
{
    public SendGridConfigurationValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        RuleFor(x => x.NewsletterListId).NotEmpty();
        RuleFor(x => x.SenderName).NotEmpty();
        RuleFor(x => x.SubjectLine).NotEmpty();
        RuleFor(x => x.SuppressionGroupId).GreaterThan(0);
        RuleFor(x => x.WebsiteAdminEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.TemplateId).NotEmpty();
    }
}
