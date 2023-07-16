using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// This class represents a comment that can be added to a <see cref="Newsletter"/>.
/// Comment are created by users.
/// </summary>
public class NewsletterComment : BaseModel
{

    /// <summary>
    /// The Id of the user who posted the comment
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// The Id of the newsletter the comment was posted to
    /// </summary>
    public int NewsletterId { get; set; }

    /// <summary>
    /// The content of the comment.
    /// Must be between 2-15.000 characters long.
    /// </summary>
    public string Content { get; set; } = null!;
}
