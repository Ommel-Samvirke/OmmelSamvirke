namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class SlideshowBlockDataDto : ContentBlockDataDto
{
    public List<string> ImageUrls { get; set; }
    public SlideshowBlockDataDto(int id, ContentBlockDto contentBlock, List<string> imageUrls) : base(id, contentBlock)
    {
        ImageUrls = imageUrls;
    }
}