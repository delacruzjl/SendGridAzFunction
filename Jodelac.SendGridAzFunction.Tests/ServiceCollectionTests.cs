using DataGenerator.Sources;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jodelac.SendGridAzFunction.Extensions.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddSendGridAzFunction_ValidApiKey_Succeeds()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(DictionaryFake.ValidInfo)
            .Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Act
        services.AddSendGridAzFunction(configuration);

        var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<ISendGridClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<SendGridClient>(client);
    }

    [Fact]
    public void AddSendGridAzFunction_InvalidApiKey_ThrowsException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(DictionaryFake.InvalidKey)
           .Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Act
        Action action = () =>
        {
            services.AddSendGridAzFunction(configuration);

            var client = services.BuildServiceProvider().GetRequiredService<ISendGridClient>();
        };

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void AddSendGridAzFunction_ContainsSendGridMessageHandler_Succeeds()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(DictionaryFake.ValidInfo)
           .Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Act
        services.AddSendGridAzFunction(configuration);

        var provider = services.BuildServiceProvider();
        var handler = provider.GetRequiredService<ISendGridMessageHandler>();

        // Assert
        Assert.NotNull(handler);
        Assert.IsType<SendGridMessageHandler>(handler);
    }

    [Fact]
    public void AddSendGridAzFunction_ContainsEmailHelper_Succeeds()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(DictionaryFake.ValidInfo)
           .Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Act
        services.AddSendGridAzFunction(configuration);

        var provider = services.BuildServiceProvider();
        var helper = provider.GetRequiredService<IEmailHelper>();

        // Assert
        Assert.NotNull(helper);
        Assert.IsType<EmailHelper>(helper);
    }

    [Fact]
    public void AddSendGridAzFunction_ContainsSubscriptionHelper_Succeeds()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(DictionaryFake.ValidInfo)
           .Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Act
        services.AddSendGridAzFunction(configuration);

        var provider = services.BuildServiceProvider();
        var helper = provider.GetRequiredService<ISubscriptionHelper>();

        // Assert
        Assert.NotNull(helper);
        Assert.IsType<SubscriptionHelper>(helper);
    }

    [Fact]
    public void AddSendGridAzFunction_EmptySettings_ShouldThrowExceptions()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(DictionaryFake.ApiKeyOnly)
            .Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        SendGridConfiguration actual = null;

        // Act
        Action action = () => {
            services.AddSendGridAzFunction(configuration);

            var option = services.BuildServiceProvider().GetRequiredService<IOptions<SendGridConfiguration>>();

            actual = option?.Value;
        };

        // assert
        Assert.Throws<ValidationException>(action);
        Assert.Null(actual);
    }
}


public static class DictionaryFake
{
    private const string FAKEAPIKEY = "SGAPIKEY";

    public static Dictionary<string, string> ValidInfo => new()
    {
        { SendGridConstants.SENDGRID_API_KEY, FAKEAPIKEY },
        { SendGridConstants.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
        { SendGridConstants.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
        { SendGridConstants.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
        { SendGridConstants.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
        { SendGridConstants.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
        { SendGridConstants.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
        { SendGridConstants.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
    };

    public static Dictionary<string, string> InvalidKey => new()
    {
        { SendGridConstants.SENDGRID_EMAIL_DYNAMIC_TEMPLATE_ID, Generator.Default.Single<EmailSource>().NextValue(null) as string  },
        { SendGridConstants.SENDGRID_NEWSLETTER_LIST_ID,  Generator.Default.Single<GuidSource>().NextValue(null).ToString()},
        { SendGridConstants.SENDGRID_SUPRESSION_GROUP_ID,  Generator.Default.Single<IntegerSource>().NextValue(null).ToString() },
        { SendGridConstants.SENDGRID_EMAIL_FROMSITE_TOSENDER_SUBJECT,  Generator.Default.Single<EmailSource>().NextValue(null)as string },
        { SendGridConstants.SENDGRID_SENDER_EMAIL_ADDRESS, Generator.Default.Single<EmailSource>().NextValue(null)as string },
        { SendGridConstants.SENDGRID_SENDER_NAME,  Generator.Default.Single<NameSource>().NextValue(null)as string },
        { SendGridConstants.WEBSITE_ADMIN_EMAIL, Generator.Default.Single<EmailSource>().NextValue(null)as string },
    };

    public static Dictionary<string, string> ApiKeyOnly => new()
    {
        { SendGridConstants.SENDGRID_API_KEY, FAKEAPIKEY }
    };
}