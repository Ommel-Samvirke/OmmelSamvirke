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
    public string Email { get; set; } = null!;

    /// <summary>
    /// Create an instance of a <see cref="NewsletterSubscriber"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="email"></param>
    public NewsletterSubscriber(string email)
    {
        Initialize(email);
    }
    
    /// <summary>
    /// Create an instance of a <see cref="NewsletterSubscriber"/>.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="email"></param>
    public NewsletterSubscriber(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string email
    ) : base(id, dateModified, dateCreated)
    {
        Initialize(email);
    }

    private void Initialize(string email)
    {
        ValidateEmail(email);

        Email = email;
    }

    private static void ValidateEmail(string email)
    {
        try
        {
            MailAddress _ = new(email);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Property Email must be a valid email format");
        }
    }
}
