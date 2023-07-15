namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class TextBlockDataDto : ContentBlockDataDto
{
    public string Text { get; set; }
    public TextBlockDataDto(int id, ContentBlockDto contentBlock, string text, PageDto page) : base(id, contentBlock, page)
    {
        Text = text;
    }
}
