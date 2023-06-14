using FluentValidation;

namespace Jodelac.SendGridAzFunction.Validators;

public class ContactFormValidator : AbstractValidator<ContactForm>
{
    public ContactFormValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required");

        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Message content is required");
    }
}