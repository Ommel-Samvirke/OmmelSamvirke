using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// This class represents the relationship between a <see cref="Newsletter"/>
/// and the <see cref="NewsletterCommunity"/> instances it's associated with.
/// A newsletter can be sent to the <see cref="MailingList"/> of multiple communities.
/// </summary>
public class NewsletterCommunityAssociations : BaseModel
{
    /// <summary>
    /// The id of an instance of a <see cref="Newsletter"/>
    /// </summary>
    public int NewsletterId { get; set; }

    /// <summary>
    /// A list of all the <see cref="NewsletterCommunity"/> instances
    /// associated with the instance of <see cref="Newsletter"/> represented by
    /// <see cref="NewsletterId"/>.
    /// </summary>
    public ISet<int> NewsletterCommunities { get; set; } = null!;

    /// <summary>
    /// Create an instance of <see cref="NewsletterCommunityAssociations"/>
    /// with an empty list of <see cref="NewsletterCommunities"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    public NewsletterCommunityAssociations(int newsletterId)
    {
        Initialize(newsletterId, new HashSet<int>());
    }
    
    /// <summary>
    /// Create an instance of <see cref="NewsletterCommunityAssociations"/>
    /// with an empty list of <see cref="NewsletterCommunities"/>.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    public NewsletterCommunityAssociations(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        int newsletterId
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(newsletterId, new HashSet<int>());
    }

    /// <summary>
    /// Create an instance of <see cref="NewsletterCommunityAssociations"/>
    /// for the newsletter with the id <paramref name="newsletterId"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    /// <param name="newsletterCommunities"><see cref="NewsletterCommunities"/></param>
    public NewsletterCommunityAssociations(int newsletterId, ISet<int> newsletterCommunities)
    {
        Initialize(newsletterId, newsletterCommunities);
    }
    
    /// <summary>
    /// Create an instance of <see cref="NewsletterCommunityAssociations"/>
    /// for the newsletter with the id <paramref name="newsletterId"/>.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    /// <param name="newsletterCommunities"><see cref="NewsletterCommunities"/></param>
    public NewsletterCommunityAssociations(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        int newsletterId, 
        ISet<int> newsletterCommunities
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(newsletterId, newsletterCommunities);
    }

    private void Initialize(int newsletterId, ISet<int> newsletterCommunities)
    {
        IntegerValidator.Validate(newsletterId, 1);

        NewsletterId = newsletterId;
        NewsletterCommunities = newsletterCommunities;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private NewsletterCommunityAssociations()
    {
        
    }
}
