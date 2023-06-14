using System.Text.Json.Serialization;

namespace Jodelac.SendGridAzFunction.Models;

public class NewsletterContact : ApiModelBase
{
    public string Email { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    public NewsletterContact()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }
}
