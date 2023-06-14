using System.Text.Json;
using FluentValidation;
using Jodelac.SendGridAzFunction.Extensions;
using Jodelac.SendGridAzFunction.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.FunctionHelpers;

public class EmailHelper : IEmailHelper
{
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IValidator<ContactForm> _validator;
    private readonly ISendGridMessageHandler _sendGridMessageHandler;
    private readonly ISendGridContactHandler _sendGridContactHandler;

    public EmailHelper(
        JsonSerializerOptions jsonOptions,
        IValidator<ContactForm> validator,
        ISendGridMessageHandler sendGridMessageHandler,
        ISendGridContactHandler sendGridContactHandler)
    {
        _jsonOptions = jsonOptions;
        _validator = validator;
        _sendGridMessageHandler = sendGridMessageHandler;
        _sendGridContactHandler = sendGridContactHandler;
    }

    public async Task QueueEmailToSendGrid(Stream body, IAsyncCollector<SendGridMessage> messageCollector, ILogger _logger)
    {
        using (_logger.BeginScope(nameof(QueueEmailToSendGrid)))
        {
            var message = await body.ConvertFrom<ContactForm>(_jsonOptions, _logger);
            await message.Validate(_validator, _logger);

            var names = message.FullName.Split(' ');
            NewsletterContact contact = new()
            {
                Email = message.Email,
                FirstName = names[0],
                LastName = names[^1]
            };

            _ = await _sendGridContactHandler.AddContactToSendGridList(contact, _logger);
            _ = await _sendGridContactHandler.AddContactToSendGridGroup(contact, _logger);

            var SendGridMessage = _sendGridMessageHandler.MakeSendGridMessage(message);
            await messageCollector.AddAsync(SendGridMessage);
        }
    }
}
