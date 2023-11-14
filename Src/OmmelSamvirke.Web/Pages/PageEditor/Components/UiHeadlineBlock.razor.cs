using Microsoft.AspNetCore.Components;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class UiHeadlineBlock
{
    private HeadlineBlock? _content;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        _content = new HeadlineBlock
        {
            Headline = "Test headline"
        };
    }
}