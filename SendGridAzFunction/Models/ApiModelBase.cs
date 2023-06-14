using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.Models;

public abstract class ApiModelBase
{
    public virtual async Task Validate<T>(IValidator<T> validator, ILogger logger) where T : class
    {
        using (logger.BeginScope("NewsletterContact.Validate"))
        {
            logger.LogInformation("Validating NewsletterContact");
            var validationResult = await validator.ValidateAsync(instance: this as T
                ?? throw new ArgumentException($"{typeof(T)} is not a valid instance"));

            if (validationResult.IsValid)
                return;

            var message = string.Join(", ", validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"));
            logger.LogError("NewsletterContact Validation failed - {Message}", message);
            throw new ArgumentException(message);
        }
    }
}
