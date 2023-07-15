namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class SlideshowBlockDataQueryDto : ContentBlockDataQueryDto
{
    public List<string> ImageUrls { get; set; }
    public SlideshowBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        List<string> imageUrls,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate, contentBlock, page)
    {
        ImageUrls = imageUrls;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public SlideshowBlockDataQueryDto()
    {
        
    }
}