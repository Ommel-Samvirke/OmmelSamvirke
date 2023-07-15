using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries;

public class PageQueryDto : BaseModel
{
    public string Name { get; set; }
    public PageTemplateQueryDto Template { get; set; }
    
    public PageQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        string name,
        PageTemplateQueryDto template
    ) : base(id, createdDate, modifiedDate)
    {
        Name = name;
        Template = template;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageQueryDto()
    {
        
    }
}
