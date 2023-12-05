using OmmelSamvirke.Web.Enums.PageEditor;
using OmmelSamvirke.Web.Pages.PageEditor.Components;

namespace OmmelSamvirke.Web.Services.PageEditor;

public class SelectedLayoutService
{
    public event Action? OnSelectedLayoutChanged;
    public event Action? OnUiBlockCollectionChanged;
    
    public PageEditorLayout? SelectedLayoutInstance;
    public List<DraggableUiBlock> SelectedLayoutUiBlocks = new();
    private PageLayout _selectedLayout = PageLayout.Desktop;
    private Dictionary<PageLayout, List<DraggableUiBlock>> _uiBlocks = new();

    public PageLayout SelectedLayout
    {
        get => _selectedLayout;
        private set
        {
            if (_selectedLayout == value) return;
            
            _selectedLayout = value;
            SelectedLayoutUiBlocks = _uiBlocks[_selectedLayout];
            OnSelectedLayoutChanged?.Invoke();
        }
    }
    
    public void SetSelectedLayout(PageLayout layout)
    {
        SelectedLayout = layout;
    }
    
    public void SetSelectedLayoutInstance(PageEditorLayout pageEditorLayout)
    {
        SelectedLayoutInstance = pageEditorLayout;
    }
    
    public void AddUiBlock(DraggableUiBlock uiBlock)
    {
        if (_uiBlocks.ContainsKey(SelectedLayout))
        {
            _uiBlocks[SelectedLayout].Add(uiBlock);
        }
        else
        {
            _uiBlocks.Add(SelectedLayout, new List<DraggableUiBlock> { uiBlock });
        }
        
        SelectedLayoutUiBlocks = _uiBlocks[SelectedLayout];
        OnUiBlockCollectionChanged?.Invoke();
    }
    
    public void UpdateUiBlock(DraggableUiBlock uiBlock)
    {
        if (_uiBlocks.ContainsKey(SelectedLayout))
        {
            int index = _uiBlocks[SelectedLayout].FindIndex(block => block.ElementId == uiBlock.ElementId);
            if (index != -1)
            {
                _uiBlocks[SelectedLayout][index] = uiBlock;
            }
        }

        SelectedLayoutUiBlocks = _uiBlocks[SelectedLayout];
        OnUiBlockCollectionChanged?.Invoke();
    }
}
