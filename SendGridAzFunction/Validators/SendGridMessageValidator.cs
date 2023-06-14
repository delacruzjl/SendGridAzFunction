using FluentValidation;

namespace SendGridAzFunction.Validators;

public class SendGridMessageValidator : AbstractValidator<ContactForm>
{
    public SendGridMessageValidator()
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