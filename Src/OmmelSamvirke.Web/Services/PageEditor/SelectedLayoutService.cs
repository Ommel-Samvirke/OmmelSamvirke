using OmmelSamvirke.Web.Enums.PageEditor;

namespace OmmelSamvirke.Web.Services.PageEditor;

public class SelectedLayoutService
{
    public event Action? OnSelectedLayoutChanged;
    private PageLayout _selectedLayout = PageLayout.Desktop;

    public PageLayout SelectedLayout
    {
        get => _selectedLayout;
        private set
        {
            if (_selectedLayout == value) return;
            
            _selectedLayout = value;
            OnSelectedLayoutChanged?.Invoke();
        }
    }
    
    public void SetSelectedLayout(PageLayout layout)
    {
        SelectedLayout = layout;
    }
}
