using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public class ImageBlockData : ContentBlockData<ImageBlock>
{
    public string ImageUrl { get; private set; } = null!;

    public ImageBlockData(ImageBlock imageBlock, string imageUrl, int pageId) : base(imageBlock, pageId)
    {
        Initialize(imageUrl);
    }
    
    public ImageBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        ImageBlock imageBlock,
        string imageUrl, 
        int pageId
    ) : base(id, dateCreated, dateModified, imageBlock, pageId)
    {
        Initialize(imageUrl);
    }

    private void Initialize(string imageUrl)
    {
        StringLengthValidator.Validate(imageUrl, 1, 2000);
        ImageUrl = imageUrl;
    }
}
