using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

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

    /// <summary>
    /// Creates a new instance of <see cref="NewsletterCommunity"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    public NewsletterCommunity(string name)
    {
        Initialize(name);
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="NewsletterCommunity"/>.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="name"><see cref="Name"/></param>
    public NewsletterCommunity(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string name
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(name);
    }

    private void Initialize(string name)
    {
        StringLengthValidator.Validate(name, 3, 35);

        Name = name;
    }
}
