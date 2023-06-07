using System.Net.Mail;
using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// This class represents a person who is subscribed to one or more
/// <see cref="MailingList"/>(s).
/// </summary>
public class NewsletterSubscriber : BaseModel
{
    /// <summary>
    /// The email address of a <see cref="NewsletterSubscriber"/>.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Create an instance of a <see cref="NewsletterSubscriber"/>.
    /// </summary>
    /// <param name="email"></param>
    public NewsletterSubscriber(string email)
    {
        ValidateEmail(email);

        Email = email;
    }

    /// <summary>
    /// Update the email address of a <see cref="NewsletterSubscriber"/>
    /// </summary>
    /// <param name="email"><see cref="Email"/></param>
    /// <exception cref="ArgumentException">Thrown if the format of <paramref name="email"/> is invalid</exception>
    public void UpdateEmail(string email)
    {
        ValidateEmail(email);

        Email = email;
    }

    private static void ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new(email);
        }
        catch (FormatException _)
        {
            throw new ArgumentException("Property Email must be a valid email format");
        }
    }
}
