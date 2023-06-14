# SendGridAzFunction

Usage:

```csharp
// 1. Register the service in your startup.cs
services.AddSendGridAzFunction(Configuration); // Configuration is an IConfiguration object

// 2. inside an azure function, call the method you desire:
...
await QueueEmailToSendGrid(req, messageCollector, _logger);
...

// OR

response = await SubscribeContactToSite(req, _logger, response);
return new StatusCodeResult(response);

```

## Required settings

```init
AzureWebJobsSendGridApiKey
SENDGRID_NEWSLETTER_LIST_ID
SENDGRID_TEMPLATE_ID
SENDGRID_EMAIL_ADDRESS
SENDGRID_SENDER_NAME
SENDGRID_SUBJECT_LINE
SENDGRID_SUPPRESSION_GROUP_ID
WEBSITE_ADMIN_EMAIL
```