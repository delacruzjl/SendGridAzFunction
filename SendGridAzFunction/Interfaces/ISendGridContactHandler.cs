using Microsoft.Extensions.Logging;

namespace SendGridAzFunction.Interfaces;

public interface ISendGridContactHandler
{
    Task<int> AddContactToSendGridGroup(NewsletterContact contact, ILogger logger);
    Task<int> AddContactToSendGridList(NewsletterContact contact, ILogger logger);
}
