namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class SlideshowBlockDataQueryDto : ContentBlockDataQueryDto
{
    public List<string> ImageUrls { get; set; } = new();
}