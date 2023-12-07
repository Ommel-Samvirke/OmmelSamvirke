using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OmmelSamvirke.Web.Models;
using OmmelSamvirke.Web.Models.PageEditor;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public abstract partial class DraggableUiBlock
{
    [Parameter] public PageEditorLayout? ParentLayout { get; set; }
    [Parameter] public (int, int) InitialDimensions { get; set; }
    [Parameter] public (double, double) InitialPosition { get; set; }
    [Parameter] public string ElementId { get; set; } = null!;

    public (int, int) Dimensions { get; protected set; }
    public (double, double) Position { get; protected set; }

    protected Dictionary<string, object> ElementStyle = new();
    
    private const double ContainerPaddingTop = 50;
    private string _containerId = string.Empty;
    private double? _mouseDownX;
    private double? _mouseDownY;
    private WindowDimensions? _windowDimensions;
    private bool _isMouseDown;
    private double _verticalDebt;
    private double _horizontalDebt;

    protected override void OnInitialized()
    {
        DraggableUiBlock? uiBlock = LayoutService
            .GetUiBlocksForLayout(LayoutService.SelectedLayout)
            .Find(x => x.ElementId == ElementId);

        if (uiBlock is not null)
        {
            Dimensions = (uiBlock.Dimensions.Item1, uiBlock.Dimensions.Item2);
            Position = (uiBlock.Position.Item1, uiBlock.Position.Item2);
        } else {
            Dimensions = (InitialDimensions.Item1, InitialDimensions.Item2);
            Position = (InitialPosition.Item1, InitialPosition.Item2);
        }
        
        _containerId = "grid-background";
        ElementStyle = new Dictionary<string, object>
        {
            ["style"] = $"width: {Dimensions.Item1}px; height: {Dimensions.Item2}px;"
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _windowDimensions = await JsRuntime.InvokeAsync<WindowDimensions>("getWindowDimensions");
            await MoveElement(Position.Item1, Position.Item2);
        }
    }

    public async Task TriggerMouseUp()
    {
        await OnMouseUp(null);
    }

    protected async Task OnMouseLeave(MouseEventArgs args)
    {
        if (!_isMouseDown)
        {
            await OnMouseUp(args);
        }
    }
    
    protected async Task OnWheel(WheelEventArgs args)
    {
        if (_isMouseDown)
        {
            _mouseDownY += args.DeltaY;
            Position = (Position.Item1, Position.Item2 + args.DeltaY);
            await MoveElement(Position.Item1, Position.Item2);
        }
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
        
        LayoutService.SelectUiBlock(this);
    }

    protected async Task OnMouseUp(MouseEventArgs? args)
    {
        _isMouseDown = false;
        
        int newPositionX = (int)Math.Round(Position.Item1 / 10.0) * 10;
        int newPositionY = (int)Math.Round(Position.Item2 / 10.0) * 10;
        
        await MoveElement(newPositionX, newPositionY);
    }

    protected async Task OnMouseMove(MouseEventArgs args)
    {
        if (!_isMouseDown) return;
        if (_mouseDownX is null || _mouseDownY is null) return;
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
            
            double newPositionX = Position.Item1 + roundedMouseTravelledX;
            double newPositionY = Position.Item2 + roundedMouseTravelledY;
            
            await MoveElement(newPositionX, newPositionY);
        }
    }

    protected void OnFocusOut()
    {
        LayoutService.DeselectUiBlock();
    }

    protected async Task OnKeyPress(KeyboardEventArgs args)
    {
        switch (args.Key)
        {
            case "ArrowUp":
                await MoveElement(Position.Item1, Position.Item2 - 10);
                break;
            case "ArrowDown":
                await MoveElement(Position.Item1, Position.Item2 + 10);
                break;
            case "ArrowLeft":
                await MoveElement(Position.Item1 - 10, Position.Item2);
                break;
            case "ArrowRight":
                await MoveElement(Position.Item1 + 10, Position.Item2);
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
            await MoveElement(Position.Item1, Position.Item2 + scrollSpeed);
            return true;
        }
        
        if (clientY <= appBarPosition.Height + topEdgeThreshold)
        {
            await JsRuntime.InvokeVoidAsync("scrollPage", _containerId, ElementId, -scrollSpeed);
            _mouseDownY += -scrollSpeed;
            await MoveElement(Position.Item1, Position.Item2 + -scrollSpeed);
            return true;
        }

        return false;
    }

    private async Task MoveElement(double newPositionX, double newPositionY)
    {
        if (ParentLayout is null) return;
        if (JsRuntime is null) return;
        
        if (newPositionX < 0) newPositionX = 0;
        if (newPositionY < 0) newPositionY = 0;
        if (newPositionX + Dimensions.Item1 > ParentLayout.CurrentDimensions.Item1)
        {
            newPositionX = ParentLayout.CurrentDimensions.Item1 - Dimensions.Item1;
        }
        if (newPositionY + Dimensions.Item2 > ParentLayout.CurrentDimensions.Item2)
        {
            ParentLayout.ExtendYDimension(10);
        }

        await HandlePotentialScrollDuringMove(newPositionY);
        
        Position = (newPositionX, newPositionY);
        LayoutService.UpdateUiBlock(this);
        
        await JsRuntime.InvokeVoidAsync("setElementPosition", ElementId, Position.Item1, Position.Item2);
        ParentLayout.TrimYDimensionEmptySpace();
    }

    private async Task HandlePotentialScrollDuringMove(double newPositionY)
    {
        if (_windowDimensions is null) return;
        
        if (_isMouseDown)
        {
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
            double distanceToTop = newPositionY - elementPosition.ScrollTop + (elementPosition.ScrollTop > ContainerPaddingTop ? ContainerPaddingTop : 0);
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
        }
    }
}
