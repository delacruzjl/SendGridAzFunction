namespace Jodelac.SendGridAzFunction.Models;

public class ContactForm : ApiModelBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }

    public ContactForm()
    {
        FullName = string.Empty;
        Email = string.Empty;
        Content = string.Empty;
    }
}