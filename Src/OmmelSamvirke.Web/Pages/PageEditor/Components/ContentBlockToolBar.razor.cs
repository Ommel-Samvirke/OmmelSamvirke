using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OmmelSamvirke.Web.Models.PageEditor;

namespace OmmelSamvirke.Web.Pages.PageEditor.Components;

public partial class ContentBlockToolBar
{
    [Parameter] public EventCallback<DraggableUiBlock> OnAddUiBlock { get; set; }

    private async Task HandleAddHeadlineBlock()
    {
        if (SelectedLayoutService.SelectedLayoutInstance is null) return;

        double xPosition = SelectedLayoutService.SelectedLayoutInstance.CurrentDimensions.Item1 / 2 - (200 / 2);
        double yPosition = await GetGridScrollTop();
        
        UiHeadlineBlock headlineBlock = new(
            initialDimensions: (200, 200),
            initialPosition: (xPosition, yPosition),
            parentLayout: SelectedLayoutService.SelectedLayoutInstance
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

    private async Task<double> GetGridScrollTop()
    {
        ContainerScroll containerScroll = await JsRuntime.InvokeAsync<ContainerScroll>(
            "getContainerScrollPosition", "grid-background"
        );

        return containerScroll.ScrollTop;
    }
}
