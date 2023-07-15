using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public abstract class ContentBlockDataQueryDto : BaseModel
{
    protected ContentBlockQueryDto ContentBlock { get; set; }
    protected PageQueryDto Page { get; set; }

    protected ContentBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate)
    {
        ContentBlock = contentBlock;
        Page = page;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public ContentBlockDataQueryDto()
    {
        
    }
}
