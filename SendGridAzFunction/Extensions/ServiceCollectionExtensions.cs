using System.Text.Json;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace Jodelac.SendGridAzFunction.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSendGridAzFunction(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidatorsFromAssemblyContaining<SendGridConfigurationValidator>();

        services.ConfigureOptions<SendGridConfigurationSetup>();
        services.AddSingleton(RegisterAppSerialization());

        var sendGridApiKey = configuration[SendGridConstants.SENDGRID_API_KEY];
        services.AddSingleton(_ => RegisterSendGridClient(sendGridApiKey));

        services.AddScoped<ISendGridContactHandler, SendGridContactHandler>();
        services.AddScoped<ISendGridMessageHandler, SendGridMessageHandler>();
        services.AddScoped<IEmailHelper, EmailHelper>();
        services.AddScoped<ISubscriptionHelper, SubscriptionHelper>();

        return services;

        ///

        Func<IServiceProvider, JsonSerializerOptions> RegisterAppSerialization() =>
            _ => new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        ISendGridClient RegisterSendGridClient(string sendGridApiKey)
        {
            if (string.IsNullOrWhiteSpace(sendGridApiKey))
            {
                ArgumentNullException argumentNullException = new(nameof(sendGridApiKey));
                throw argumentNullException;
            }

            return new SendGridClient(sendGridApiKey);
        }
    }
}
