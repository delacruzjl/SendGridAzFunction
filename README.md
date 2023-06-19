# SendGridAzFunction

Usage:

1. Install the package in your Azure Function project.

```bash
dotnet add package Jodelac.SendGridAzFunction
```

3. Setup dependency injection for Azure Functions

4. Add the require keys to your settings

```init
AzureWebJobsSendGridApiKey = string
SendGrid:NewsletterListId= string
SendGrid:TemplateId= string
SendGrid:EmailAddress= string
SendGrid:SenderName= string
SendGrid:SubjectLine= string
SendGrid:SuppressionGroupId = int
SendGrid:WebsiteAdminEmail= string
```
   
4. Register the services

```csharp
// Startup.cs
// ...
services.AddSendGridAzFunction(Configuration); // Configuration is an IConfiguration object
// ...
```

## To add a contact to your newsletter

```csharp
private readonly ISubscriptionHelper _subscriptionHelper;

ctor(ISubscriptionHelper subscriptionHelper) { // ctor = constructor of your Azure Function
  _subscriptionHelper = subscriptionHelper
}

public async Task Subscribe(
  [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req
  ILogger _logger) {            
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            int response = await _subscriptionHelper.SubscribeContactToSite(req.Body, _logger); // response = HttpStatusCode
            return new StatusCodeResult(response);
}
```

## To send an email with SendGrid

```csharp
// ...
private readonly IEmailHelper _emailHelper;

ctor(IEmailHelper emailHelper) { // ctor = constructor of your Azure Function
  _emailHelper = emailHelper;
}

public async Task Send(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, 
        [SendGrid] IAsyncCollector<SendGridMessage> messageCollector,
        ILogger _logger) {            
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            await _emailHelper.QueueEmailToSendGrid(req.Body, messageCollector, _logger);        
    }
```


