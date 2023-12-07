namespace OmmelSamvirke.Web.Services.PageEditor;

public class PropertiesPanelService
{
    public event Action? OnOpenPropertiesPanel;
    public event Action? OnClosePropertiesPanel;
    
    public void Open()
    {
        OnOpenPropertiesPanel?.Invoke();
    }
    
    public void Close()
    {
        OnClosePropertiesPanel?.Invoke();
    }
}
