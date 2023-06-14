using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.Interfaces
{
    public interface IEmailHelper
    {
        Task QueueEmailToSendGrid(Stream body, IAsyncCollector<SendGridMessage> messageCollector, ILogger _logger);
    }
}