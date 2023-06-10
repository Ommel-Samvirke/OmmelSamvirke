using Ganss.Xss;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

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
    public int UserId { get; private set; }
    
    /// <summary>
    /// The Id of the newsletter the comment was posted to
    /// </summary>
    public int NewsletterId { get; private set; }

    /// <summary>
    /// The content of the comment.
    /// Must be between 2-15.000 characters long.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Create an instance of a newsletter comment.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="userId"><see cref="UserId"/></param>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    /// <param name="content"><see cref="Content"/></param>
    public NewsletterComment(int userId, int newsletterId, string content)
    {
        Initialize(userId, newsletterId, content);
    }
    
    /// <summary>
    /// Create an instance of a newsletter comment.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="userId"><see cref="UserId"/></param>
    /// <param name="newsletterId"><see cref="NewsletterId"/></param>
    /// <param name="content"><see cref="Content"/></param>
    public NewsletterComment(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        int userId,
        int newsletterId,
        string content
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(userId, newsletterId, content);
    }

    private void Initialize(int userId, int newsletterId, string content)
    {
        HtmlSanitizer htmlSanitizer = new();
        string sanitizedContent = htmlSanitizer.Sanitize(content);

        StringLengthValidator.Validate(sanitizedContent, 2, 15_000);
        
        UserId = userId;
        NewsletterId = newsletterId;
        Content = sanitizedContent;
    }
}
