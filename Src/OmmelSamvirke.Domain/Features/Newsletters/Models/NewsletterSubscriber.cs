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
}
