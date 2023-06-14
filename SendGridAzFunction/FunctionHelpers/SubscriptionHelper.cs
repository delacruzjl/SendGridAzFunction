using System.Text.Json;
using FluentValidation;
using Jodelac.SendGridAzFunction.Extensions;
using Jodelac.SendGridAzFunction.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.FunctionHelpers;

public class SubscriptionHelper
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

    public async Task<int> SubscribeContactToSite(HttpRequest req, ILogger _logger, int response)
    {
        using (_logger.BeginScope("NewsletterSubscriber"))
        {
            var contact = await req.Body.ConvertFrom<NewsletterContact>(_jsonOptions, _logger);
            await contact.Validate(_validator, _logger);

            _ = await _sendGridContactHandler.AddContactToSendGridList(contact, _logger);
            response = await _sendGridContactHandler.AddContactToSendGridGroup(contact, _logger);
        }

        return response;
    }
}
