namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class HeadlineBlockDataDto : ContentBlockDataDto
{
    public string Headline { get; set; }
    
    public HeadlineBlockDataDto(int id, ContentBlockDto contentBlock, string headline, PageDto page) : base(id, contentBlock, page)
    {
        Headline = headline;
    }
}
