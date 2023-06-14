using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Jodelac.SendGridAzFunction.Extensions;

public static class StreamExtensions
{
    public static async Task<T> ConvertFrom<T>(this Stream body, JsonSerializerOptions jsonOptions, ILogger logger)
    {
        using (logger.BeginScope("StreamExtensions.ConvertFrom"))
        {
            logger.LogInformation("Converting stream to NewsletterContact");

            var requestBody = await new StreamReader(body).ReadToEndAsync();
            Activity.Current?.AddBaggage("requestBody", requestBody);

            if (!string.IsNullOrWhiteSpace(requestBody))
                return JsonSerializer.Deserialize<T>(requestBody, jsonOptions) ??
                    throw new ArgumentNullException(nameof(body));

            logger.LogError("Request body is empty");
            throw new ArgumentNullException(nameof(body));
        }
    }
}