using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// A NewsletterCommunity represents a community that can send newsletters
/// to a set of <see cref="NewsletterSubscriber"/> instances. Each subscriber
/// is a person that has subscribed to newsletters from this community.
/// </summary>
public class NewsletterCommunity : BaseModel
{
    /// <summary>
    /// The name of the <see cref="NewsletterCommunity"/>. Must be between 3-35 characters long.
    /// <example>Ommel Samvirke</example>
    /// </summary>
    public string Name { get; set; } = null!;
}
