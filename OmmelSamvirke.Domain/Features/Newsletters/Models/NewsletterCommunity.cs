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
    /// The name of the <see cref="NewsletterCommunity"/>.
    /// <example>Ommel Samvirke</example>
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Creates a new instance of <see cref="NewsletterCommunity"/>.
    /// The <paramref name="name"/> must be between 3-15 characters long.
    /// </summary>
    /// <param name="name">The name of the <see cref="NewsletterCommunity"/></param>
    public NewsletterCommunity(string name)
    {
        ValidateName(name);
        
        Name = name;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Property Name cannot be null or empty");
        }

        if (name.Length is < 3 or > 35)
        {
            throw new ArgumentException("Property Name must be between 3-35 characters long");
        }
    }
}
