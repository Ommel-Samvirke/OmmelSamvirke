namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class ImageBlockDataQueryDto : ContentBlockDataQueryDto
{
    public string ImageUrl { get; set; }
    public ImageBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        string imageUrl,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate, contentBlock, page)
    {
        ImageUrl = imageUrl;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public ImageBlockDataQueryDto()
    {
        
    }
}
