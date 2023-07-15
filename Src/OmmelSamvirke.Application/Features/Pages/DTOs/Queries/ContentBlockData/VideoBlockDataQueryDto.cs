namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class VideoBlockDataQueryDto : ContentBlockDataQueryDto
{
    public string VideoUrl { get; set; }
    public VideoBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        string videoUrl,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate, contentBlock, page)
    {
        VideoUrl = videoUrl;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public VideoBlockDataQueryDto()
    {
        
    }
}
