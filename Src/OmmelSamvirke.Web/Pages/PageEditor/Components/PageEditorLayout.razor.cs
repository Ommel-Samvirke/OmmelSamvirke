﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class PageEditorLayout
{
    [Parameter] public (int, int) InitialDimensions { get; set; }
    
    private List<DraggableUiBlock> _uiBlocks = new();
    private DraggableUiBlock UiBlock
    {
        set => _uiBlocks.Add(value);
    }
    
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
        _currentDimensions = InitialDimensions;
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
        foreach (DraggableUiBlock uiBlock in _uiBlocks)
        {
            await uiBlock.TriggerMouseUp();
        }
    }

    private async Task OnMouseUp(MouseEventArgs args)
    {
        foreach (DraggableUiBlock uiBlock in _uiBlocks)
        {
            await uiBlock.TriggerMouseUp();
        }
    }
    
    private void ExtendYDimension(int deltaY)
    {
        _currentDimensions = (_currentDimensions.Item1, _currentDimensions.Item2 + deltaY);
        _gridStyle["style"] = $"width: {_currentDimensions.Item1}px; height: {_currentDimensions.Item2}px;";
    }
    
    private void TrimYDimensionEmptySpace()
    {
        double highestYPosition = 0;
        
        foreach (DraggableUiBlock uiBlock in _uiBlocks)
        {
            int height = uiBlock.Height;
            double yPosition = uiBlock.Position.Item2;
            double yPositionPlusHeight = yPosition + height;
            
            if (yPositionPlusHeight > highestYPosition)
            {
                highestYPosition = yPositionPlusHeight;
            }
        }
        
        if (highestYPosition < InitialDimensions.Item2)
        {
            _currentDimensions = (_currentDimensions.Item1, InitialDimensions.Item2);
            _gridStyle["style"] = $"width: {_currentDimensions.Item1}px; height: {_currentDimensions.Item2}px;";
        }
        else
        {
            _currentDimensions = (_currentDimensions.Item1, (int)highestYPosition);
            _gridStyle["style"] = $"width: {_currentDimensions.Item1}px; height: {_currentDimensions.Item2}px;";
        }
    }
}