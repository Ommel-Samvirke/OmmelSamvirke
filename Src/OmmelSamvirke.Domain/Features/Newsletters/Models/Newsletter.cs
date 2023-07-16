using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// This class represents a newsletter which can be sent on behalf of one
/// or more <see cref="NewsletterCommunity"/> instance(s). The newsletter
/// is sent to the <see cref="NewsletterSubscriber"/>(s) on the relevant
/// <see cref="MailingList"/>(s).
/// </summary>
public class Newsletter : BaseModel
{
    /// <summary>
    /// Describes the title of the newsletter.
    /// This will be the title field in an email client.
    /// Must be 5-200 characters long.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// The HTML content that will be displayed by email clients
    /// that can render HTML emails. Must be 50-15.000 characters long
    /// </summary>
    public string HtmlContent { get; set; } = null!;

    /// <summary>
    /// The plain text content that will be displayed by email clients
    /// that cannot render HTML emails. 50-15.000 characters long
    /// </summary>
    public string PlainContent { get; set; } = null!;

    /// <summary>
    /// The Id of the Admin that created the newsletter.
    /// Only the Admin that creates a newsletter, can send the newsletter.
    /// </summary>
    public int AdminId { get; set; }

    /// <summary>
    /// The Date and time when the newsletter was sent.
    /// </summary>
    public DateTime? SentDate { get; set; }
    
    /// <summary>
    /// The number of users who have liked the newsletter
    /// </summary>
    public int Likes { get; set; }
}
