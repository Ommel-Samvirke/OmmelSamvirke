using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// A MailingList contains information about which <see cref="NewsletterSubscribers"/>
/// that have subscribed to newsletters from a <see cref="NewsletterCommunity"/>.
/// </summary>
public class MailingList : BaseModel
{
    /// <summary>
    /// The model id of the <see cref="NewsletterCommunity"/> that owns the
    /// mailing list.
    /// </summary>
    public int NewsletterCommunityId { get; set; }

    /// <summary>
    /// A list of <see cref="NewsletterSubscribers"/> that are subscribed to
    /// the mailing list.
    /// </summary>
    public ISet<NewsletterSubscriber> NewsletterSubscribers { get; set; } = new HashSet<NewsletterSubscriber>();
}
