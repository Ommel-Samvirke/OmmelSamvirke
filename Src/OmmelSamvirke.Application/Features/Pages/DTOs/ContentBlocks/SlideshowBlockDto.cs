using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;

public class SlideshowBlockDto : ContentBlockDto
{
    public List<Url> ImageUrls { get; set; } = new();
}
