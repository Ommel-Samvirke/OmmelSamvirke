using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageTemplateQueryDto : BaseModel
{
    public string Name { get; set;  }
    public List<ContentBlockQueryDto> ContentBlocks { get; set; }
    public PageTemplateState State { get; set;  }

    public PageTemplateQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        string name,
        List<ContentBlockQueryDto> contentBlocks,
        PageTemplateState pageTemplateState
    ) : base(id, createdDate, modifiedDate)
    {
        Name = name;
        ContentBlocks = contentBlocks;
        State = pageTemplateState;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageTemplateQueryDto()
    {
        
    }
}
