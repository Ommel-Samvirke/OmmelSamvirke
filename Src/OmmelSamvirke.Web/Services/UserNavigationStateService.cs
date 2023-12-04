using OmmelSamvirke.Web.Routing;

namespace OmmelSamvirke.Web.Services;

public class UserNavigationStateService
{
    public event Action? OnCurrentPageChanged;

    private PageEnum _currentPageEnum;
    public PageEnum CurrentPageEnum
    {
        get => _currentPageEnum;
        private set
        {
            if (_currentPageEnum == value) return;
            
            _currentPageEnum = value;
            OnCurrentPageChanged?.Invoke();
        }
    }

    public void SetCurrentPage(PageEnum pageEnum)
    {
        CurrentPageEnum = pageEnum;
    }
}
