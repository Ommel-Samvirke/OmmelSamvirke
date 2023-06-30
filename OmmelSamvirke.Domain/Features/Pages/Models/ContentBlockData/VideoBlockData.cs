using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public class VideoBlockData : ContentBlockData<VideoBlock>
{
    public string VideoUrl { get; private set; } = null!;

    public VideoBlockData(VideoBlock videoBlock, string videoUrl, int pageId) : base(videoBlock, pageId)
    {
        Initialize(videoUrl);
    }
    
    public VideoBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        VideoBlock videoBlock,
        string videoUrl, 
        int pageId
    ) : base(id, dateCreated, dateModified, videoBlock, pageId)
    {
        Initialize(videoUrl);
    }

    private void Initialize(string videoUrl)
    {
        StringLengthValidator.Validate(videoUrl, 1, 2000);
        VideoUrl = videoUrl;
    }
}
