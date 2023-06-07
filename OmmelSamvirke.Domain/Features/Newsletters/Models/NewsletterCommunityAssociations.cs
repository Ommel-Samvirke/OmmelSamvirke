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
    public int NewsletterId { get; }
    
    /// <summary>
    /// A list of all the <see cref="NewsletterCommunity"/> instances
    /// associated with the instance of <see cref="Newsletter"/> represented by
    /// <see cref="NewsletterId"/>.
    /// </summary>
    public IList<int> NewsletterCommunities { get; set; }

    /// <summary>
    /// Create an instance of <see cref="NewsletterCommunityAssociations"/>
    /// with an empty list of <see cref="NewsletterCommunities"/>.
    /// </summary>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    public NewsletterCommunityAssociations(int newsletterId)
    {
        ModelIdValidator.Validate(newsletterId);

        NewsletterId = newsletterId;
        NewsletterCommunities = new List<int>();
    }

    /// <summary>
    /// Create an instance of <see cref="NewsletterCommunityAssociations"/>
    /// for the newsletter with the id <paramref name="newsletterId"/>.
    /// </summary>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    /// <param name="newsletterCommunities"><see cref="NewsletterCommunities"/></param>
    public NewsletterCommunityAssociations(int newsletterId, IList<int> newsletterCommunities)
    {
        ModelIdValidator.Validate(newsletterId);
        
        NewsletterId = newsletterId;
        NewsletterCommunities = newsletterCommunities;
    }
}
