using System.Net;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

[TestFixture]
public class GetPageTemplates : BaseWebClientProvider
{
    private PagesFixture _pagesFixture = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        using IServiceScope scope = Factory.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _pagesFixture = new PagesFixture(dbContext);

        _pagesFixture.PopulateDatabase();
    }
    
    [Test]
    public async Task GetById_WhenIdExists_ReturnsPageTemplate()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates/1");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}
