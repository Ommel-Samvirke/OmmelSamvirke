using Microsoft.AspNetCore.Components;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class UiHeadlineBlock
{
    private HeadlineBlock? _content;

    public UiHeadlineBlock()
    {
    }

    public UiHeadlineBlock((int, int) initialDimensions, (double, double) initialPosition, PageEditorLayout? parentLayout)
    {
        InitialDimensions = initialDimensions;
        InitialPosition = initialPosition;
        ParentLayout = parentLayout;
        ElementId = Guid.NewGuid().ToString();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        _content = new HeadlineBlock
        {
            Headline = "Test headline"
        };
    }
}