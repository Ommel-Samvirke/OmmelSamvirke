namespace OmmelSamvirke.Domain.Features.ContentSharing.Models;

public class FacebookShareOptions
{
    /// <summary>
    /// The title to be displayed on the Facebook post.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The summary to be displayed on the Facebook post.
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the image to be displayed on the Facebook post.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
}
