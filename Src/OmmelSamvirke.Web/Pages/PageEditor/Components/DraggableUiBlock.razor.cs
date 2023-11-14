using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OmmelSamvirke.Web.Models;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public abstract partial class DraggableUiBlock
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public (int, int) ContainerDimensions { get; set; }

    protected string ElementId = string.Empty;
    protected Dictionary<string, object> ElementStyle = new();
    
    private string _containerId = string.Empty;
    private double? _mouseDownX;
    private double? _mouseDownY;
    private double _containerPaddingTop = 50;
    private (double, double) _position = (0, 0);
    private readonly int _width = 200;
    private readonly int _height = 200;
    private WindowDimensions? _windowDimensions;
    private bool _isMouseDown;
    private double _verticalDebt;
    private double _horizontalDebt;

    protected override void OnInitialized()
    {
        ElementId = Guid.NewGuid().ToString();
        _containerId = "grid-background";
        ElementStyle = new Dictionary<string, object>
        {
            ["style"] = $"width: {_width}px; height: {_height}px;"
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _windowDimensions = await JsRuntime.InvokeAsync<WindowDimensions>("getWindowDimensions");
    }

    protected async Task OnMouseDown(MouseEventArgs args)
    {
        ElementPositionWithContainer elementPosition = await JsRuntime.InvokeAsync<ElementPositionWithContainer>(
            "getElementPositionWithContainer",
            ElementId,
            _containerId
        );
        
        _mouseDownX = args.ClientX + elementPosition.ScrollLeft;
        _mouseDownY = args.ClientY + elementPosition.ScrollTop;
        _isMouseDown = true;
    }

    protected async Task OnMouseUp(MouseEventArgs args)
    {
        _isMouseDown = false;
        
        int newPositionX = (int)Math.Round(_position.Item1 / 10.0) * 10;
        int newPositionY = (int)Math.Round(_position.Item2 / 10.0) * 10;

        await MoveElement(newPositionX, newPositionY);
    }

    protected async Task OnMouseMove(MouseEventArgs args)
    {
        if (_mouseDownX is null || _mouseDownY is null) return;
        if (!_isMouseDown) return;
        if (await ScrollIfCloseToEdge(args.ClientY)) return;
        
        ElementPositionWithContainer elementPosition = await JsRuntime.InvokeAsync<ElementPositionWithContainer>(
            "getElementPositionWithContainer",
            ElementId,
            _containerId
        );

        double clientX = args.ClientX + elementPosition.ScrollLeft;
        double clientY = args.ClientY + elementPosition.ScrollTop;

        double mouseTravelledX = clientX - _mouseDownX.Value;
        double mouseTravelledY = clientY - _mouseDownY.Value;

        if (Math.Abs(mouseTravelledX) >= 10 || Math.Abs(mouseTravelledY) >= 10)
        {
            double roundedMouseTravelledX = Math.Round(mouseTravelledX / 10.0) * 10;
            double roundedMouseTravelledY = Math.Round(mouseTravelledY / 10.0) * 10;
            
            _horizontalDebt += mouseTravelledX - roundedMouseTravelledX;
            _verticalDebt += mouseTravelledY - roundedMouseTravelledY;

            ResolvePositionalDebt(ref roundedMouseTravelledX, ref roundedMouseTravelledY);

            _mouseDownX += roundedMouseTravelledX;
            _mouseDownY += roundedMouseTravelledY;
            
            double newPositionX = _position.Item1 + roundedMouseTravelledX;
            double newPositionY = _position.Item2 + roundedMouseTravelledY;
            
            await MoveElement(newPositionX, newPositionY);
        }
    }

    protected async Task OnKeyPress(KeyboardEventArgs args)
    {
        switch (args.Key)
        {
            case "ArrowUp":
                await MoveElement(_position.Item1, _position.Item2 - 10);
                break;
            case "ArrowDown":
                await MoveElement(_position.Item1, _position.Item2 + 10);
                break;
            case "ArrowLeft":
                await MoveElement(_position.Item1 - 10, _position.Item2);
                break;
            case "ArrowRight":
                await MoveElement(_position.Item1 + 10, _position.Item2);
                break;
        }
    }
    
    private void ResolvePositionalDebt(ref double roundedMouseTravelledX, ref double roundedMouseTravelledY)
    {
        while (_horizontalDebt >= 10)
        {
            roundedMouseTravelledX += 10;
            _horizontalDebt -= 10;
        }

        while (_verticalDebt >= 10)
        {
            roundedMouseTravelledY += 10;
            _verticalDebt -= 10;
        }
    }

    private async Task<bool> ScrollIfCloseToEdge(double clientY)
    {
        if (_windowDimensions is null) return false;
        
        ElementPosition appBarPosition = await JsRuntime.InvokeAsync<ElementPosition>(
            "getElementPosition",
            "appBar"
        );
        
        const int scrollSpeed = 25;
        const int bottomEdgeThreshold = 75;
        const int topEdgeThreshold = 50;
        double distanceToBottom = _windowDimensions.Height - clientY;

        if (distanceToBottom <= bottomEdgeThreshold)
        {
            await JsRuntime.InvokeVoidAsync("scrollPage", _containerId, ElementId, scrollSpeed);

            _mouseDownY += scrollSpeed;
            await MoveElement(_position.Item1, _position.Item2 + scrollSpeed);
            return true;
        }
        
        if (clientY <= appBarPosition.Height + topEdgeThreshold)
        {
            await JsRuntime.InvokeVoidAsync("scrollPage", _containerId, ElementId, -scrollSpeed);
            _mouseDownY += -scrollSpeed;
            await MoveElement(_position.Item1, _position.Item2 + -scrollSpeed);
            return true;
        }

        return false;
    }

    private async Task MoveElement(double newPositionX, double newPositionY)
    {
        if (_windowDimensions is null) return;
        
        if (newPositionX < 0) newPositionX = 0;
        if (newPositionY < 0) newPositionY = 0;
        if (newPositionX + _width > ContainerDimensions.Item1) newPositionX = ContainerDimensions.Item1 - _width;
        if (newPositionY + _height > ContainerDimensions.Item2) newPositionY = ContainerDimensions.Item2 - _height;
        
        ElementPositionWithContainer elementPosition = await JsRuntime.InvokeAsync<ElementPositionWithContainer>(
            "getElementPositionWithContainer",
            ElementId,
            _containerId
        );
        ElementPosition appBarPosition = await JsRuntime.InvokeAsync<ElementPosition>(
            "getElementPosition",
            "appBar"
        );
        
        double distanceToBottom = _windowDimensions.Height - appBarPosition.Height - (newPositionY - elementPosition.ScrollTop);
        double distanceToTop = newPositionY - elementPosition.ScrollTop + (elementPosition.ScrollTop > _containerPaddingTop ? _containerPaddingTop : 0);
        const int scrollSpeed = 10;
        
        if (distanceToBottom < 100)
        {
            await JsRuntime.InvokeVoidAsync("scrollPage", _containerId, ElementId, scrollSpeed);
        }

        if (distanceToTop < -20)
        {
            await JsRuntime.InvokeVoidAsync("scrollPage", _containerId, ElementId, -scrollSpeed);
            if (newPositionY == 0)
            {
                await JsRuntime.InvokeVoidAsync("scrollPage", _containerId, ElementId, -100);
            }
        }
        
        _position = (newPositionX, newPositionY);
        await JsRuntime.InvokeVoidAsync("setElementPosition", ElementId, _position.Item1, _position.Item2);
    }
}
