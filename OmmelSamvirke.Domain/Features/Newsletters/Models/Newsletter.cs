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
    public string Title { get; }
    
    /// <summary>
    /// The HTML content that will be displayed by email clients
    /// that can render HTML emails. Must be 50-15.000 characters long
    /// </summary>
    public string HtmlContent { get; }
    
    /// <summary>
    /// The plain text content that will be displayed by email clients
    /// that cannot render HTML emails. 50-15.000 characters long
    /// </summary>
    public string PlainContent { get; }
    
    /// <summary>
    /// The Id of the Admin that created the newsletter.
    /// Only the Admin that creates a newsletter, can send the newsletter.
    /// </summary>
    public int AdminId { get; }
    
    /// <summary>
    /// The Date and time when the newsletter was sent.
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// Create an instance of a newsletter that has not been sent.
    /// </summary>
    /// <param name="title"><see cref="Title"/></param>
    /// <param name="htmlContent"><see cref="HtmlContent"/></param>
    /// <param name="plainContent"><see cref="PlainContent"/></param>
    /// <param name="adminId"><see cref="AdminId"/></param>
    public Newsletter(string title, string htmlContent, string plainContent, int adminId)
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
    }

    /// <summary>
    /// Create an instance of a newsletter that has been sent.
    /// </summary>
    /// <param name="title"><see cref="Title"/></param>
    /// <param name="htmlContent"><see cref="HtmlContent"/></param>
    /// <param name="plainContent"><see cref="PlainContent"/></param>
    /// <param name="adminId"><see cref="AdminId"/></param>
    /// <param name="sentDate"><see cref="SentDate"/></param>
    public Newsletter(string title, string htmlContent, string plainContent, int adminId, DateTime sentDate)
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
