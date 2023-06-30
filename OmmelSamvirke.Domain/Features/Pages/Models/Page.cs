using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.Features.Pages.Models;

public class Page : BaseModel
{
    public string Name { get; private set; } = null!;
    public PageTemplate Template { get; private set; } = null!;

    public Page(string name, PageTemplate template)
    {
        Initialize(name, template);
    }
    
    public Page(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        string name,
        PageTemplate template
    ) : base(id, dateCreated, dateModified)
    {
        Initialize(name, template);
    }

    private void Initialize(string name, PageTemplate template)
    {
        StringLengthValidator.Validate(name, 1, 100);
        Name = name;
        Template = template;
    }
}
