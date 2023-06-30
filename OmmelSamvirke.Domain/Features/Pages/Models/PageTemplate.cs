using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

public class PageTemplate : BaseModel
{
    public string Name { get; private set; } = null!;
    public List<string> SupportedLayouts { get; private set; } = new();
    public List<ContentBlock> Blocks { get; private set; } = new();
    public PageTemplateState State { get; private set; }

    public PageTemplate(string name, List<string> supportedLayouts, PageTemplateState state)
    {
        Initialize(name, supportedLayouts, state);
    }
    
    public PageTemplate(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string name,
        List<string> supportedLayouts,
        PageTemplateState state
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(name, supportedLayouts, state);
    }

    private void Initialize(string name, List<string> supportedLayouts, PageTemplateState state)
    {
        StringLengthValidator.Validate(name, 1, 100);
        Name = name;
        SupportedLayouts = supportedLayouts;
        State = state;
    }
}
