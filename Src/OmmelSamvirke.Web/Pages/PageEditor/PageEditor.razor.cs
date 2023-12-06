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
        SelectedLayoutService.OnSelectedLayoutChanged += StateHasChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (SelectedLayoutService.SelectedLayout == PageLayout.Desktop && _desktopLayout is not null)
        {
            SelectedLayoutService.SetSelectedLayoutInstance(_desktopLayout);
        }
        
        if (SelectedLayoutService.SelectedLayout == PageLayout.Tablet && _tabletLayout is not null)
        {
            SelectedLayoutService.SetSelectedLayoutInstance(_tabletLayout);
        }
        
        if (SelectedLayoutService.SelectedLayout == PageLayout.Mobile && _mobileLayout is not null)
        {
            SelectedLayoutService.SetSelectedLayoutInstance(_mobileLayout);
        }
    } 

    private void AddUiBlockToLayout(DraggableUiBlock uiBlock)
    {
        switch (SelectedLayoutService.SelectedLayout)
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
        SelectedLayoutService.OnSelectedLayoutChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
