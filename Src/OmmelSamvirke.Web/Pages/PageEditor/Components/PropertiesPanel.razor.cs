namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class PropertiesPanel
{
    private bool _isDrawerOpen;

    protected override void OnInitialized()
    {
        PropertiesPanelService.OnOpenPropertiesPanel += OnOpenDrawer;
        PropertiesPanelService.OnClosePropertiesPanel += OnCloseDrawer;
    }
    
    private void OnOpenDrawer()
    {
        _isDrawerOpen = true;
        StateHasChanged();
    }
    
    private void OnCloseDrawer()
    {
        _isDrawerOpen = false;
        StateHasChanged();
    }

    public void Dispose()
    {
        PropertiesPanelService.OnOpenPropertiesPanel -= OnOpenDrawer;
        PropertiesPanelService.OnClosePropertiesPanel -= OnCloseDrawer;
    }
}
