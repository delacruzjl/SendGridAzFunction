using System.Text.Json;
using FluentValidation;
using Jodelac.SendGridAzFunction.Extensions;
using Jodelac.SendGridAzFunction.Interfaces;
using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.FunctionHelpers;

public class SubscriptionHelper : ISubscriptionHelper
{
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IValidator<NewsletterContact> _validator;
    private readonly ISendGridContactHandler _sendGridContactHandler;

    public SubscriptionHelper(
        JsonSerializerOptions jsonOptions,
        IValidator<NewsletterContact> validator,
        ISendGridContactHandler sendGridContactHandler)
    {
        _jsonOptions = jsonOptions;
        _validator = validator;
        _sendGridContactHandler = sendGridContactHandler;
    }

    public async Task<int> SubscribeContactToSite(Stream body, ILogger _logger)
    {
        using (_logger.BeginScope("NewsletterSubscriber"))
        {
            var contact = await body.ConvertFrom<NewsletterContact>(_jsonOptions, _logger);
            await contact.ValidateAsync(_validator, _logger);

            _ = await _sendGridContactHandler.AddContactToSendGridList(contact, _logger);
            return await _sendGridContactHandler.AddContactToSendGridGroup(contact, _logger);
        }
    }
}
