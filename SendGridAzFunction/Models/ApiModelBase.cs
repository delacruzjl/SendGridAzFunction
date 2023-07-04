using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.Models;

public abstract class ApiModelBase
{
    public virtual async Task ValidateAsync<T>(IValidator<T> validator, ILogger logger) where T : class
    {
        using (logger.BeginScope("NewsletterContact.Validate"))
        {
            logger.LogInformation("Validating NewsletterContact");
            await validator.ValidateAndThrowAsync(
                instance: this as T
               ?? throw new ArgumentException($"{typeof(T)} is not a valid instance"));
        }
    }
}
