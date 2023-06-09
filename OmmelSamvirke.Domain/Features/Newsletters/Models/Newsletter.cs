using OmmelSamvirke.Domain.Common.Validators;
using Ganss.Xss;
using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Newsletters.Models;

/// <summary>
/// This class represents a newsletter which can be sent on behalf of one
/// or more <see cref="NewsletterCommunity"/> instance(s). The newsletter
/// is sent to the <see cref="NewsletterSubscriber"/>(s) on the relevant
/// <see cref="MailingList"/>(s).
/// </summary>
public class Newsletter : BaseModel
{
    /// <summary>
    /// Describes the title of the newsletter.
    /// This will be the title field in an email client.
    /// Must be 5-200 characters long.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// The HTML content that will be displayed by email clients
    /// that can render HTML emails. Must be 50-15.000 characters long
    /// </summary>
    public string HtmlContent { get; set; } = null!;

    /// <summary>
    /// The plain text content that will be displayed by email clients
    /// that cannot render HTML emails. 50-15.000 characters long
    /// </summary>
    public string PlainContent { get; set; } = null!;

    /// <summary>
    /// The Id of the Admin that created the newsletter.
    /// Only the Admin that creates a newsletter, can send the newsletter.
    /// </summary>
    public int AdminId { get; set; }

    /// <summary>
    /// The Date and time when the newsletter was sent.
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// Create an instance of a newsletter that has not been sent.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="title"><see cref="Title"/></param>
    /// <param name="htmlContent"><see cref="HtmlContent"/></param>
    /// <param name="plainContent"><see cref="PlainContent"/></param>
    /// <param name="adminId"><see cref="AdminId"/></param>
    public Newsletter(string title, string htmlContent, string plainContent, int adminId)
    {
        Initialize(title, htmlContent, plainContent, adminId, sentDate: null);
    }
    
    /// <summary>
    /// Create an instance of a newsletter that has not been sent.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="title"><see cref="Title"/></param>
    /// <param name="htmlContent"><see cref="HtmlContent"/></param>
    /// <param name="plainContent"><see cref="PlainContent"/></param>
    /// <param name="adminId"><see cref="AdminId"/></param>
    public Newsletter(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string title,
        string htmlContent,
        string plainContent,
        int adminId
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(title, htmlContent, plainContent, adminId, sentDate: null);
    }

    /// <summary>
    /// Create an instance of a newsletter that has been sent.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="title"><see cref="Title"/></param>
    /// <param name="htmlContent"><see cref="HtmlContent"/></param>
    /// <param name="plainContent"><see cref="PlainContent"/></param>
    /// <param name="adminId"><see cref="AdminId"/></param>
    /// <param name="sentDate"><see cref="SentDate"/></param>
    public Newsletter(string title, string htmlContent, string plainContent, int adminId, DateTime sentDate)
    {
        Initialize(title, htmlContent, plainContent, adminId, sentDate);
    }
    
    /// <summary>
    /// Create an instance of a newsletter that has been sent.
    /// This constructor should be used when the model is being loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="title"><see cref="Title"/></param>
    /// <param name="htmlContent"><see cref="HtmlContent"/></param>
    /// <param name="plainContent"><see cref="PlainContent"/></param>
    /// <param name="adminId"><see cref="AdminId"/></param>
    /// <param name="sentDate"><see cref="SentDate"/></param>
    public Newsletter(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string title,
        string htmlContent,
        string plainContent,
        int adminId,
        DateTime sentDate
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(title, htmlContent, plainContent, adminId, sentDate);
    }

    private void Initialize(string title, string htmlContent, string plainContent, int adminId, DateTime? sentDate)
    {
        HtmlSanitizer sanitizer = new();
        string sanitizedHtmlContent = sanitizer.Sanitize(htmlContent);

        ValidateTitle(title);
        ValidateHtmlContent(sanitizedHtmlContent);
        ValidatePlainContent(plainContent);
        ModelIdValidator.Validate(adminId);

        Title = title;
        HtmlContent = sanitizedHtmlContent;
        PlainContent = plainContent;
        AdminId = adminId;
        SentDate = sentDate;
    }
    
    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new ArgumentException("Property HTMLContent cannot be null or empty");
        }

        if (title.Length is < 5 or > 200)
        {
            throw new ArgumentException("Property Title must be between 5-200 characters long");
        }
    }

    private static void ValidateHtmlContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("Property HTMLContent cannot be null or empty");
        }

        if (content.Length is < 50 or > 15_000)
        {
            throw new ArgumentException("Property HTMLContent must be between 50-15.000 characters long");
        }
    }

    private static void ValidatePlainContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("Property PlainContent cannot be null or empty");
        }

        if (content.Length is < 50 or > 15_000)
        {
            throw new ArgumentException("Property PlainContent must be between 50-15.000 characters long");
        }
    }
}
