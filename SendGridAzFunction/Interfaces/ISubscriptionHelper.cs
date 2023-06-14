using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.Interfaces
{
    public interface ISubscriptionHelper
    {
        Task<int> SubscribeContactToSite(HttpRequest req, ILogger _logger, int response);
    }
}