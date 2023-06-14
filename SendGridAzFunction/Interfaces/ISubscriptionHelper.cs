using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.Interfaces
{
    public interface ISubscriptionHelper
    {
        Task<int> SubscribeContactToSite(Stream body, ILogger _logger, int response);
    }
}