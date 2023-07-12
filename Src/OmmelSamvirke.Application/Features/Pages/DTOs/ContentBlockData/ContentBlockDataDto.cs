namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public abstract class ContentBlockDataDto
{
    private int Id { get; set; }
    private ContentBlockDto ContentBlock { get; set; }

    protected ContentBlockDataDto(int id, ContentBlockDto contentBlock)
    {
        Id = id;
        ContentBlock = contentBlock;
    }
}
