using System.Text.Json;
using FluentValidation;
using Jodelac.SendGridAzFunction.Handlers;
using Jodelac.SendGridAzFunction.Interfaces;
using Jodelac.SendGridAzFunction.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace Jodelac.SendGridAzFunction.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSendGridAzFunction(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(_ => new SendGridConfiguration(configuration));
        services.AddSingleton(_ => new SendGridConfiguration(configuration));
        services.AddSingleton(new SendGridConfiguration(configuration));
        services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        });

        var sendGridApiKey = configuration[SendGridConfiguration.SENDGRID_API_KEY];
        services.AddValidatorsFromAssemblyContaining<NewsletterContactValidator>();
        services.AddSingleton<ISendGridClient>(new SendGridClient(sendGridApiKey));
        services.AddScoped<ISendGridContactHandler, SendGridContactHandler>();
        services.AddScoped<ISendGridMessageHandler, SendGridMessageHandler>();

        return services;
    }
}
