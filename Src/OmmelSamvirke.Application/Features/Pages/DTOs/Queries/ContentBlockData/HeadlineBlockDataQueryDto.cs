namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class HeadlineBlockDataQueryDto : ContentBlockDataQueryDto
{
    public string Headline { get; set; }
    
    public HeadlineBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        string headline,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate, contentBlock, page)
    {
        Headline = headline;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public HeadlineBlockDataQueryDto()
    {
        
    }
}
