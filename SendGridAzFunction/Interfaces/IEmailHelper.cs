using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace Jodelac.SendGridAzFunction.Interfaces
{
    public interface IEmailHelper
    {
        Task QueueEmailToSendGrid(HttpRequest req, IAsyncCollector<SendGridMessage> messageCollector, ILogger _logger);
    }
}