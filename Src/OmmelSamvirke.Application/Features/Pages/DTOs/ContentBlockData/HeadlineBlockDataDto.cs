namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class HeadlineBlockDataDto : ContentBlockDataDto
{
    public string Headline { get; set; }
    
    public HeadlineBlockDataDto(int id, ContentBlockDto contentBlock, string headline) : base(id, contentBlock)
    {
        Headline = headline;
    }
}
