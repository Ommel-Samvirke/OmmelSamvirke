using JetBrains.Annotations;

namespace OmmelSamvirke.Web.Pages.PageEditor;

[UsedImplicitly]
public partial class PageEditor
{
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SelectedLayoutService.OnSelectedLayoutChanged += StateHasChanged;
    }

    public void Dispose()
    {
        SelectedLayoutService.OnSelectedLayoutChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
