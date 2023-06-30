using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public class SlideshowBlockData : ContentBlockData<SlideshowBlock>
{
    public List<string> ImageUrls { get; private set; } = null!;

    public SlideshowBlockData(
        SlideshowBlock slideshowBlock,
        List<string> imageUrls,
        int pageId
    ) : base(slideshowBlock, pageId)
    {
        Initialize(imageUrls);
    }
    
    public SlideshowBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        SlideshowBlock slideshowBlock,
        List<string> imageUrls,
        int pageId
    ) : base(id, dateCreated, dateModified, slideshowBlock, pageId)
    {
        Initialize(imageUrls);
    }

    private void Initialize(List<string> imageUrls)
    {
        ImageUrls = imageUrls;
    }
}
