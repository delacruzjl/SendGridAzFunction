using DataGenerator;
using DataGenerator.Sources;
using Jodelac.SendGridAzFunction.FunctionHelpers;
using Jodelac.SendGridAzFunction.Handlers;
using Jodelac.SendGridAzFunction.Interfaces;
using Jodelac.SendGridAzFunction.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace Jodelac.SendGridAzFunction.Extensions.Tests;

public class ServiceCollectionExtensionsTests
{
    private readonly string apiKey = "SGAPIKEY";

    [Fact]
    public void AddSendGridAzFunction_ValidApiKey_Succeeds()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { SendGridConfiguration.SENDGRID_API_KEY, apiKey },
                { SendGridConfiguration.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
                { SendGridConfiguration.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
                { SendGridConfiguration.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
                { SendGridConfiguration.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
                { SendGridConfiguration.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
            })
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddSendGridAzFunction(configuration);

        // Assert
        var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<ISendGridClient>();
        Assert.NotNull(client);
        Assert.IsType<SendGridClient>(client);
    }

    [Fact]
    public void AddSendGridAzFunction_InvalidApiKey_ThrowsException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(new Dictionary<string, string>
           {
                { SendGridConfiguration.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
                { SendGridConfiguration.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
                { SendGridConfiguration.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
                { SendGridConfiguration.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
                { SendGridConfiguration.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
           })
           .Build();
        var services = new ServiceCollection();

        // Act
        Action action = () => services.AddSendGridAzFunction(configuration);

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void AddSendGridAzFunction_ContainsSendGridMessageHandler_Succeeds()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(new Dictionary<string, string>
           {
                { SendGridConfiguration.SENDGRID_API_KEY, apiKey },
                { SendGridConfiguration.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
                { SendGridConfiguration.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
                { SendGridConfiguration.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
                { SendGridConfiguration.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
                { SendGridConfiguration.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
           })
           .Build();
        var services = new ServiceCollection();

        // Act
        services.AddSendGridAzFunction(configuration);

        // Assert
        var provider = services.BuildServiceProvider();
        var handler = provider.GetRequiredService<ISendGridMessageHandler>();
        Assert.NotNull(handler);
        Assert.IsType<SendGridMessageHandler>(handler);
    }

    [Fact]
    public void AddSendGridAzFunction_ContainsEmailHelper_Succeeds()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(new Dictionary<string, string>
           {
                { SendGridConfiguration.SENDGRID_API_KEY, apiKey },
                { SendGridConfiguration.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
                { SendGridConfiguration.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
                { SendGridConfiguration.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
                { SendGridConfiguration.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
                { SendGridConfiguration.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
           })
           .Build();
        var services = new ServiceCollection();

        // Act
        services.AddSendGridAzFunction(configuration);

        // Assert
        var provider = services.BuildServiceProvider();
        var helper = provider.GetRequiredService<IEmailHelper>();
        Assert.NotNull(helper);
        Assert.IsType<EmailHelper>(helper);
    }

    [Fact]
    public void AddSendGridAzFunction_ContainsSubscriptionHelper_Succeeds()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(new Dictionary<string, string>
           {
                { SendGridConfiguration.SENDGRID_API_KEY, apiKey },
                { SendGridConfiguration.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
                { SendGridConfiguration.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
                { SendGridConfiguration.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
                { SendGridConfiguration.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
                { SendGridConfiguration.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
                { SendGridConfiguration.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
           })
           .Build();
        var services = new ServiceCollection();

        // Act
        services.AddSendGridAzFunction(configuration);

        // Assert
        var provider = services.BuildServiceProvider();
        var helper = provider.GetRequiredService<ISubscriptionHelper>();
        Assert.NotNull(helper);
        Assert.IsType<SubscriptionHelper>(helper);
    }

    [Fact]
    public void AddSendGridAzFunction_EmptySettings_ShouldThrowExceptions()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { SendGridConfiguration.SENDGRID_API_KEY, apiKey }
            })
            .Build();
        var services = new ServiceCollection();

        // Act
        Assert.Throws<AggregateException>(() => services.AddSendGridAzFunction(configuration));
    }
}
