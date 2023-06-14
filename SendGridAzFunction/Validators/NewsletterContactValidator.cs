using FluentValidation;

namespace SendGridAzFunction.Validators;

public class NewsletterContactValidator : AbstractValidator<NewsletterContact>
{
    public NewsletterContactValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}