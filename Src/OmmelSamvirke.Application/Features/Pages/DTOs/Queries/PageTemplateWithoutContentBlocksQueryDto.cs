using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageTemplateWithoutContentBlocksQueryDto : BaseModel
{
    public string Name { get; set;  }
    public PageTemplateState PageTemplateState { get; set;  }

    public PageTemplateWithoutContentBlocksQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        string name,
        PageTemplateState pageTemplateState
    ) : base(id, createdDate, modifiedDate)
    {
        Name = name;
        PageTemplateState = pageTemplateState;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageTemplateWithoutContentBlocksQueryDto()
    {
        
    }
}
