using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Pages;

public class PostPagesTests : BaseWebClientProvider
{
    private static PagesFixture _pagesFixture = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        using IServiceScope scope = Factory.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _pagesFixture = new PagesFixture(dbContext);
    }
    
    
    
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}