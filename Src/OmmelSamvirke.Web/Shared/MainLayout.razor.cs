using Microsoft.AspNetCore.Components.Routing;
using OmmelSamvirke.Web.Routing;

namespace OmmelSamvirke.Web.Shared;

public partial class MainLayout
{
    protected override void OnInitialized()
    {
        NavManager.LocationChanged += HandleLocationChanged;
        UserNavigationStateService.OnCurrentPageChanged += StateHasChanged;
        
        Uri uri = new(NavManager.Uri);
        string path = Uri.UnescapeDataString(uri.AbsolutePath);
        UpdateCurrentPage(path);
    }

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        Uri uri = new(e.Location);
        string path = Uri.UnescapeDataString(uri.AbsolutePath);
        
        UpdateCurrentPage(path);
    }

    private void UpdateCurrentPage(string path)
    {
        PageEnum? page = GetPageByPath(path);

        if (!page.HasValue) return;
        if (page.Value == UserNavigationStateService.CurrentPageEnum) return;

        UserNavigationStateService.SetCurrentPage(page.Value);
    }

    private static PageEnum? GetPageByPath(string path)
    {
        foreach (PageEnum page in Enum.GetValues(typeof(PageEnum)))
        {
            string? pagePath = page.GetPath();
            if (pagePath != null && pagePath.Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                return page;
            }
        }
        
        return null;
    }

    public void Dispose()
    {
        NavManager.LocationChanged -= HandleLocationChanged;
        UserNavigationStateService.OnCurrentPageChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}