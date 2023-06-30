using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public class HeadlineBlockData : ContentBlockData<HeadlineBlock>
{
    public string Headline { get; private set; } = null!;

    public HeadlineBlockData(HeadlineBlock headlineBlock, string headline, int pageId) : base(headlineBlock, pageId)
    {
        Initialize(headline);
    }

    public HeadlineBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        HeadlineBlock headlineBlock,
        string headline, 
        int pageId
    ) : base(id, dateCreated, dateModified, headlineBlock, pageId)
    {
        Initialize(headline);
    }
    
    private void Initialize(string headline)
    {
        StringLengthValidator.Validate(headline, 1, 200);
        Headline = headline;
    }
}
