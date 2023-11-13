using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OmmelSamvirke.Web.Pages;

public partial class PageEditor
{
    private readonly (int, int) _initialDimensions = (1600, 4000);
    private (int, int) _currentDimensions;
    private Dictionary<string, object> gridStyle;
    
    private double zoomLevel = 1;
    private const double ZoomFactor = 0.1;
    private const double MinZoomLevel = 0.5;
    private bool preventScrolling;

    protected override void OnInitialized()
    {
        _currentDimensions = _initialDimensions;
        gridStyle = new Dictionary<string, object>
        {
            ["style"] = $"width: {_currentDimensions.Item1}px; height: {_currentDimensions.Item2}px;"
        };
    }
    
    private void HandleScroll(WheelEventArgs e)
    {
        if (!e.ShiftKey) return;

        switch (e.DeltaY)
        { 
            // Scrolling up
            case < 0:
                zoomLevel += ZoomFactor;
                break;
            // Scrolling down
            case > 0:
                zoomLevel = Math.Max(MinZoomLevel, zoomLevel - ZoomFactor);
                break;
        }
        
        UpdateGridDimensions(zoomLevel);
    }

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        preventScrolling = e.ShiftKey;
    }
    
    private void UpdateGridDimensions(double zoom)
    {
        double newWidth = _initialDimensions.Item1 * zoom;
        double newHeight = _initialDimensions.Item2 * zoom;
        double gridSizeMain = 40 * zoom;
        double gridSizeSub = 10 * zoom; 

        gridStyle["style"] = $"width: {newWidth}px; height: {newHeight}px; " +
                             $"background-size: {gridSizeMain}px {gridSizeMain}px, " +
                             $"{gridSizeMain}px {gridSizeMain}px, " +
                             $"{gridSizeSub}px {gridSizeSub}px, " +
                             $"{gridSizeSub}px {gridSizeSub}px;";
        
        StateHasChanged();
    }
}
