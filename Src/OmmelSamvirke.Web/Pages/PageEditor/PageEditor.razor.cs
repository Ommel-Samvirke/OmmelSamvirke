using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OmmelSamvirke.Web.Pages.PageEditor;

public partial class PageEditor
{
    private readonly (int, int) _initialDimensions = (1600, 4000);
    private (int, int) _currentDimensions;
    private Dictionary<string, object> _gridStyle = new();
    
    private double _zoomLevel = 1;
    private const double ZoomFactor = 0.1;
    private const double MinZoomLevel = 0.5;
    private bool _preventScrolling;
    
    private string _containerId = string.Empty;
    
    protected override void OnInitialized()
    {
        _containerId = "grid-background";
        _currentDimensions = _initialDimensions;
        _gridStyle = new Dictionary<string, object>
        {
            ["style"] = $"width: {_currentDimensions.Item1}px; height: {_currentDimensions.Item2}px;"
        };
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("preventArrowKeyScroll", _containerId);
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
        double newWidth = _initialDimensions.Item1 * zoom;
        double newHeight = _initialDimensions.Item2 * zoom;
        double gridSizeMain = 40 * zoom;
        double gridSizeSub = 10 * zoom; 

        _gridStyle["style"] = $"width: {newWidth}px; height: {newHeight}px; " +
                             $"background-size: {gridSizeMain}px {gridSizeMain}px, " +
                             $"{gridSizeMain}px {gridSizeMain}px, " +
                             $"{gridSizeSub}px {gridSizeSub}px, " +
                             $"{gridSizeSub}px {gridSizeSub}px;";
        
        StateHasChanged();
    }
}
