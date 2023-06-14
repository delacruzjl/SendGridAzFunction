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