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
    public int NewsletterCommunityId { get; set; }

    /// <summary>
    /// A list of <see cref="NewsletterSubscribers"/> that are subscribed to
    /// the mailing list.
    /// </summary>
    public ISet<NewsletterSubscriber> NewsletterSubscribers { get; set; } = null!;

    /// <summary>
    /// Creates an empty mailing list for the <see cref="NewsletterCommunity"/>
    /// with the provided <paramref name="newsletterCommunityId"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="newsletterCommunityId"><see cref="NewsletterCommunityId"/></param>
    public MailingList(int newsletterCommunityId)
    {
        Initialize(newsletterCommunityId, new HashSet<NewsletterSubscriber>());
    }

    /// <summary>
    /// Creates an empty mailing list for the <see cref="NewsletterCommunity"/>
    /// with the provided <paramref name="newsletterCommunityId"/>.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="newsletterCommunityId"><see cref="NewsletterCommunityId"/></param>
    public MailingList(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        int newsletterCommunityId
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(newsletterCommunityId, new HashSet<NewsletterSubscriber>());
    }

    /// <summary>
    /// Creates a mailing list for the <see cref="NewsletterCommunity"/>
    /// with the provided <paramref name="newsletterCommunityId"/>. The mailing list
    /// is populated with the provided <paramref name="newsletterSubscribers"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="newsletterCommunityId"><see cref="NewsletterCommunityId"/></param>
    /// <param name="newsletterSubscribers"><see cref="NewsletterSubscribers"/></param>
    public MailingList(int newsletterCommunityId, ISet<NewsletterSubscriber> newsletterSubscribers)
    {
        Initialize(newsletterCommunityId, newsletterSubscribers);
    }
    
    /// <summary>
    /// Creates a mailing list for the <see cref="NewsletterCommunity"/>
    /// with the provided <paramref name="newsletterCommunityId"/>. The mailing list
    /// is populated with the provided <paramref name="newsletterSubscribers"/>.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="newsletterCommunityId"><see cref="NewsletterCommunityId"/></param>
    /// <param name="newsletterSubscribers"><see cref="NewsletterSubscribers"/></param>
    public MailingList(
        int id, 
        DateTime dateCreated, 
        DateTime dateModified, 
        int newsletterCommunityId, 
        ISet<NewsletterSubscriber> newsletterSubscribers
    ): base(id, dateCreated, dateModified)
    {
        Initialize(newsletterCommunityId, newsletterSubscribers);
    }

    private void Initialize(int newsletterCommunityId, ISet<NewsletterSubscriber> newsletterSubscribers)
    {
        IntegerValidator.Validate(newsletterCommunityId, 1);

        NewsletterCommunityId = newsletterCommunityId;
        NewsletterSubscribers = newsletterSubscribers;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private MailingList()
    {
        
    }
}
