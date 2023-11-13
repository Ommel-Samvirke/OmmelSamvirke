using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class UIHeadlineBlock
{
    private HeadlineBlock _content;

    protected override void OnInitialized()
    {
        _content = new HeadlineBlock
        {
            Headline = "Test headline"
        };
    }
}