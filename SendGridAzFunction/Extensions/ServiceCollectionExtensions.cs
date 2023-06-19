using System.Text.Json;
using FluentValidation;
using Jodelac.SendGridAzFunction.FunctionHelpers;
using Jodelac.SendGridAzFunction.Handlers;
using Jodelac.SendGridAzFunction.Interfaces;
using Jodelac.SendGridAzFunction.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid;

namespace Jodelac.SendGridAzFunction.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSendGridAzFunction(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SendGridConfiguration>(
            configuration.GetSection(key: "SendGrid"));
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

        services.AddScoped<IEmailHelper, EmailHelper>();
        services.AddScoped<ISubscriptionHelper, SubscriptionHelper>();

        var config = services.BuildServiceProvider().GetService<IOptionsMonitor<SendGridConfiguration>>()
            ?? throw new ArgumentException("SendGrid configuration must be setup");

        var configValidator = services.BuildServiceProvider().GetService<IValidator<SendGridConfiguration>>()
            ?? throw new ArgumentException("No validator for sendgrid configuration found");

        config?.CurrentValue?.ValidateAsync(configValidator).Wait();

        return services;
    }
}
