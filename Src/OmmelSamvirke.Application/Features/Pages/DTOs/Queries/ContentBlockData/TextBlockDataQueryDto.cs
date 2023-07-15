namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class TextBlockDataQueryDto : ContentBlockDataQueryDto
{
    public string Text { get; set; }
    public TextBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        string text,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate, contentBlock, page)
    {
        Text = text;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public TextBlockDataQueryDto()
    {
        
    }
}
