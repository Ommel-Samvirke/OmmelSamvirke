using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OmmelSamvirke.Web.Models.PageEditor;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class ContentBlockToolBar
{
    [Parameter] public EventCallback<DraggableUiBlock> OnAddUiBlock { get; set; }

    private string _toggleGridLabel = "Skjul gitter";
    
    private async Task HandleAddHeadlineBlock()
    {
        if (LayoutService.SelectedLayoutInstance is null) return;

        const int initialWidth = 200;
        const int initialHeight = 200;
        
        double initialXPosition = LayoutService.SelectedLayoutInstance.CurrentDimensions.Item1 / 2 - initialWidth / 2;
        double initialYPosition = await GetGridScrollTop();
        
        UiHeadlineBlock headlineBlock = new(
            initialDimensions: (initialWidth, initialHeight),
            initialPosition: (initialXPosition, initialYPosition),
            parentLayout: LayoutService.SelectedLayoutInstance
        );
        
        await OnAddUiBlock.InvokeAsync(headlineBlock);
    }
    
    private async Task HandleAddTextBlock()
    {
        await OnAddUiBlock.InvokeAsync();
    }
    
    private async Task HandleAddImageBlock()
    {
        await OnAddUiBlock.InvokeAsync();
    }
    
    private async Task HandleAddPdfBlock()
    {
        await OnAddUiBlock.InvokeAsync();
    }
    
    private async Task HandleAddVideoBlock()
    {
        await OnAddUiBlock.InvokeAsync();
    }

    private async Task HandleAddGalleryBlock()
    {
        await OnAddUiBlock.InvokeAsync();
    }
    
    private void ToggleGrid()
    {
        LayoutService.ToggleGrid();
        _toggleGridLabel = _toggleGridLabel.Equals("Vis gitter") ? "Skjul gitter" : "Vis gitter";
    }

    private async Task<double> GetGridScrollTop()
    {
        ContainerScroll containerScroll = await JsRuntime.InvokeAsync<ContainerScroll>(
            "getContainerScrollPosition", "grid-background"
        );

        return containerScroll.ScrollTop;
    }
}
