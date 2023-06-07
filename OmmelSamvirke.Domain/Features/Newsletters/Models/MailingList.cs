using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

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
    public int NewsletterCommunityId { get; }
    
    /// <summary>
    /// A list of <see cref="NewsletterSubscribers"/> that are subscribed to
    /// the mailing list.
    /// </summary>
    public IList<NewsletterSubscriber> NewsletterSubscribers { get; set; }

    /// <summary>
    /// Creates an empty mailing list for the <see cref="NewsletterCommunity"/>
    /// with the provided <paramref name="newsletterCommunityId"/>.
    /// </summary>
    /// <param name="newsletterCommunityId"><see cref="NewsletterCommunityId"/></param>
    public MailingList(int newsletterCommunityId)
    {
        ModelIdValidator.Validate(newsletterCommunityId);
        
        NewsletterCommunityId = newsletterCommunityId;
        NewsletterSubscribers = new List<NewsletterSubscriber>();
    }

    /// <summary>
    /// Creates a mailing list for the <see cref="NewsletterCommunity"/>
    /// with the provided <paramref name="newsletterCommunityId"/>. The mailing list
    /// is populated with the provided <paramref name="newsletterSubscribers"/>.
    /// </summary>
    /// <param name="newsletterCommunityId"><see cref="NewsletterCommunityId"/></param>
    /// <param name="newsletterSubscribers"><see cref="NewsletterSubscribers"/></param>
    public MailingList(int newsletterCommunityId, IList<NewsletterSubscriber> newsletterSubscribers)
    {
        ModelIdValidator.Validate(newsletterCommunityId);
        
        NewsletterCommunityId = newsletterCommunityId;
        NewsletterSubscribers = newsletterSubscribers;
    }
}
