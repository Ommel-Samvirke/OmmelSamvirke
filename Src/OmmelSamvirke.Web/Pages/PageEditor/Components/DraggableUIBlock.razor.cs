using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using OmmelSamvirke.Web.Models;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class DraggableUIBlock
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    private string _elementId = string.Empty;
    private double? _mouseOffsetX = null;
    private double? _mouseOffsetY = null;
    private double? _mouseDownX = null;
    private double? _mouseDownY = null;
    private (int, int) _position = (0, 0);
    private bool _isMouseDown;
    
    protected override void OnInitialized()
    {
        _elementId = Guid.NewGuid().ToString();
    }

    private async Task OnMouseDown(MouseEventArgs args)
    {
        ElementPosition elementPosition = await JsRuntime.InvokeAsync<ElementPosition>("getElementPosition", _elementId);
        _mouseOffsetX = args.ClientX - elementPosition.Left;
        _mouseOffsetY = args.ClientY - elementPosition.Top;
        _mouseDownX = args.ClientX;
        _mouseDownY = args.ClientY;
        _isMouseDown = true;
    }
    
    private async Task OnMouseUp(MouseEventArgs args)
    {
        _mouseOffsetX = null;
        _mouseOffsetY = null;
        _isMouseDown = false;
        
        int newPositionX = (int)Math.Round(_position.Item1 / 10.0) * 10;
        int newPositionY = (int)Math.Round(_position.Item2 / 10.0) * 10;

        _position = (newPositionX, newPositionY);
        await JsRuntime.InvokeVoidAsync("setElementPosition", _elementId, _position.Item1, _position.Item2);
    }

    private async Task OnMouseMove(MouseEventArgs args)
    {
        if (_mouseOffsetX is null || _mouseOffsetY is null) return;
        if (_mouseDownX is null || _mouseDownY is null) return;
        if (!_isMouseDown) return;
        
        double clientX = args.ClientX;
        double clientY = args.ClientY;

        double mouseTravelledX = clientX - _mouseDownX.Value;
        double mouseTravelledY = clientY - _mouseDownY.Value;
        
        if (Math.Abs(mouseTravelledX) >= 10 || Math.Abs(mouseTravelledY) >= 10)
        {
            _mouseDownX += mouseTravelledX;
            _mouseDownY += mouseTravelledY;
            
            int newPositionX = (int)(_position.Item1 + Math.Round(mouseTravelledX / 10) * 10);
            int newPositionY = (int)(_position.Item2 + Math.Round(mouseTravelledY / 10) * 10);
            
            _position = (newPositionX, newPositionY);
            await JsRuntime.InvokeVoidAsync("setElementPosition", _elementId, _position.Item1, _position.Item2);
        }
    }
}
