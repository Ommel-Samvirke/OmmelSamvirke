using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.Features.ContentSharing.Models;

public class FacebookShareOptions
{
    /// <summary>
    /// The title to be displayed on the Facebook post.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The summary to be displayed on the Facebook post.
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// The URL of the image to be displayed on the Facebook post.
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Constructs a new FacebookShareOptions with the provided title, summary and image URL.
    /// </summary>
    /// <param name="title">The title to be displayed on the Facebook post.</param>
    /// <param name="summary">The summary to be displayed on the Facebook post.</param>
    /// <param name="imageUrl">The URL of the image to be displayed on the Facebook post.</param>
    public FacebookShareOptions(string title, string summary, string imageUrl)
    {
        StringLengthValidator.Validate(title, 3, 200);
        StringLengthValidator.Validate(summary, 10, 2_500);
        StringLengthValidator.Validate(imageUrl, 5, 2_000);
        
        Title = title;
        Summary = summary;
        ImageUrl = imageUrl;
    }
}
