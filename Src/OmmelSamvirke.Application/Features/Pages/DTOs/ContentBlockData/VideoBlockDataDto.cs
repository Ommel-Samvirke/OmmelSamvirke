namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class VideoBlockDataDto : ContentBlockDataDto
{
    public string VideoUrl { get; set; }
    public VideoBlockDataDto(int id, ContentBlockDto contentBlock, string videoUrl, PageDto page) : base(id, contentBlock, page)
    {
        VideoUrl = videoUrl;
    }
}
