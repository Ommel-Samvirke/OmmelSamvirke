using JetBrains.Annotations;
using OmmelSamvirke.Web.Enums.PageEditor;
using OmmelSamvirke.Web.Pages.PageEditor.Components;

namespace OmmelSamvirke.Web.Pages.PageEditor;

[UsedImplicitly]
public partial class PageEditor
{
    private PageEditorLayout? _desktopLayout;
    private PageEditorLayout? _tabletLayout;
    private PageEditorLayout? _mobileLayout;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        LayoutService.OnSelectedLayoutChanged += StateHasChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (LayoutService.SelectedLayout == PageLayout.Desktop && _desktopLayout is not null)
        {
            LayoutService.SetSelectedLayoutInstance(_desktopLayout);
        }
        
        if (LayoutService.SelectedLayout == PageLayout.Tablet && _tabletLayout is not null)
        {
            LayoutService.SetSelectedLayoutInstance(_tabletLayout);
        }
        
        if (LayoutService.SelectedLayout == PageLayout.Mobile && _mobileLayout is not null)
        {
            LayoutService.SetSelectedLayoutInstance(_mobileLayout);
        }
    } 

    private void AddUiBlockToLayout(DraggableUiBlock uiBlock)
    {
        switch (LayoutService.SelectedLayout)
        {
            case PageLayout.Desktop:
                _desktopLayout?.AddUiBlock(uiBlock);
                break;
            case PageLayout.Tablet:
                _tabletLayout?.AddUiBlock(uiBlock);
                break;
            case PageLayout.Mobile:
                _mobileLayout?.AddUiBlock(uiBlock);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Dispose()
    {
        LayoutService.OnSelectedLayoutChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
