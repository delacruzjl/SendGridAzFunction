using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Jodelac.SendGridAzFunction.ConfigurationOptions;

public class SendGridConfigurationSetup : IConfigureOptions<SendGridConfiguration>
{
    private static string ConfigName => "SendGrid";
    private readonly IConfiguration _configuration;
    private readonly IValidator<SendGridConfiguration> _validator;

    public SendGridConfigurationSetup(IConfiguration configuration, IValidator<SendGridConfiguration> validator)
    {
        _configuration = configuration;
        _validator = validator;
    }

    public void Configure(SendGridConfiguration options)
    {
        _configuration.GetSection(ConfigName).Bind(options);
        _validator.ValidateAndThrow(options);
    }
}
