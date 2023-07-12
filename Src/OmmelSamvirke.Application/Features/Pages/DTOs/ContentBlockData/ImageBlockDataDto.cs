namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class ImageBlockDataDto : ContentBlockDataDto
{
    public string ImageUrl { get; set; }
    public ImageBlockDataDto(int id, ContentBlockDto contentBlock, string imageUrl) : base(id, contentBlock)
    {
        ImageUrl = imageUrl;
    }
}
