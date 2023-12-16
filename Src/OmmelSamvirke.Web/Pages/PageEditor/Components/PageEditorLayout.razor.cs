using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class PageEditorLayout
{
    [Parameter] public (int, int) InitialDimensions { get; set; }
    public (int, int) CurrentDimensions { get; private set; }

    private Dictionary<string, object> _gridStyle = new();

    private double _zoomLevel = 1;
    private const double ZoomFactor = 0.1;
    private const double MinZoomLevel = 0.5;
    private bool _preventScrolling;
    private bool _isGridVisible = true;
    
    private string _containerId = string.Empty;
    
    protected override void OnInitialized()
    {
        LayoutService.OnUiBlockCollectionChanged += StateHasChanged;
        LayoutService.OnGridVisibilityChanged += ToggleGridVisibility;

        _containerId = "grid-background";
        CurrentDimensions = InitialDimensions;
        _gridStyle = new Dictionary<string, object>
        {
            ["style"] = $"width: {CurrentDimensions.Item1}px; height: {CurrentDimensions.Item2}px;"
        };
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("preventArrowKeyScroll", _containerId);
        }
    }
    
    public void AddUiBlock(DraggableUiBlock uiBlock)
    {
        LayoutService.AddUiBlock(uiBlock);
        StateHasChanged();
    }

    public void ExtendYDimension(int deltaY)
    {
        CurrentDimensions = (CurrentDimensions.Item1, CurrentDimensions.Item2 + deltaY);
        _gridStyle["style"] = $"width: {CurrentDimensions.Item1}px; height: {CurrentDimensions.Item2}px;";
    }
    
    public void TrimYDimensionEmptySpace()
    {
        double highestYPosition = 0;
        
        foreach (DraggableUiBlock uiBlock in LayoutService.SelectedLayoutUiBlocks)
        {
            int height = uiBlock.Dimensions.Item2;
            double yPosition = uiBlock.Position.Item2;
            double yPositionPlusHeight = yPosition + height;
            
            if (yPositionPlusHeight > highestYPosition)
            {
                highestYPosition = yPositionPlusHeight;
            }
        }
        
        if (highestYPosition < InitialDimensions.Item2)
        {
            CurrentDimensions = (CurrentDimensions.Item1, InitialDimensions.Item2);
            _gridStyle["style"] = $"width: {CurrentDimensions.Item1}px; height: {CurrentDimensions.Item2}px;";
        }
        else
        {
            CurrentDimensions = (CurrentDimensions.Item1, (int)highestYPosition);
            _gridStyle["style"] = $"width: {CurrentDimensions.Item1}px; height: {CurrentDimensions.Item2}px;";
        }
    }

    private void HandleScroll(WheelEventArgs e)
    {
        if (!e.ShiftKey) return;

        switch (e.DeltaY)
        { 
            // Scrolling up
            case < 0:
                _zoomLevel += ZoomFactor;
                break;
            // Scrolling down
            case > 0:
                _zoomLevel = Math.Max(MinZoomLevel, _zoomLevel - ZoomFactor);
                break;
        }
        
        UpdateGridDimensions(_zoomLevel);
    }

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        _preventScrolling = e.ShiftKey;
    }
    
    private void UpdateGridDimensions(double zoom)
    {
        double newWidth = InitialDimensions.Item1 * zoom;
        double newHeight = InitialDimensions.Item2 * zoom;
        double gridSizeMain = 40 * zoom;
        double gridSizeSub = 10 * zoom; 

        _gridStyle["style"] = $"width: {newWidth}px; height: {newHeight}px; " +
                             $"background-size: {gridSizeMain}px {gridSizeMain}px, " +
                             $"{gridSizeMain}px {gridSizeMain}px, " +
                             $"{gridSizeSub}px {gridSizeSub}px, " +
                             $"{gridSizeSub}px {gridSizeSub}px;";
        
        StateHasChanged();
    }

    private async Task OnMouseLeave(MouseEventArgs args)
    {
        for (int i = 0; i < LayoutService.SelectedLayoutUiBlocks.Count; i++)
        {
            DraggableUiBlock uiBlock = LayoutService.SelectedLayoutUiBlocks[i];
            await uiBlock.TriggerMouseUp();
        }
    }

    private async Task OnMouseUp(MouseEventArgs args)
    {
        for (int i = 0; i < LayoutService.SelectedLayoutUiBlocks.Count; i++)
        {
            DraggableUiBlock uiBlock = LayoutService.SelectedLayoutUiBlocks[i];
            await uiBlock.TriggerMouseUp();
        }
    }
    
    private void ToggleGridVisibility()
    {
        _isGridVisible = !_isGridVisible;
        StateHasChanged();
    }

    public void Dispose()
    {
        LayoutService.DeselectUiBlock();
        LayoutService.OnUiBlockCollectionChanged -= StateHasChanged;
        LayoutService.OnGridVisibilityChanged -= ToggleGridVisibility;
        GC.SuppressFinalize(this);
    }
}
