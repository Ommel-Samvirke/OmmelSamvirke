using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public abstract class ContentBlockData<T>: BaseModel where T : ContentBlock
{
    public T ContentBlock { get; private set; } = null!;
    public int PageId { get; private set; }

    public ContentBlockData(T headlineBlock, int pageId)
    {
        Initialize(headlineBlock, pageId);
    }
    
    public ContentBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        T headlineBlock,
        int pageId
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(headlineBlock, pageId);
    }

    private void Initialize(T contentBlock, int pageId)
    {
        ContentBlock = contentBlock;
        PageId = pageId;
    }
}
