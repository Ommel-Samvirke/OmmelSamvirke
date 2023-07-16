using OmmelSamvirke.Domain.Common;

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
    public ISet<int> NewsletterCommunities { get; set; } = new HashSet<int>();
}
