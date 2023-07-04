namespace Jodelac.SendGridAzFunction.Models;

public class ContactForm : ApiModelBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
    public ContactForm(string fullName, string email, string content)
    {
        FullName = fullName;
        Email = email;
        Content = content;
    }
    public ContactForm()
        : this(string.Empty, string.Empty, string.Empty)
    {

    }
}