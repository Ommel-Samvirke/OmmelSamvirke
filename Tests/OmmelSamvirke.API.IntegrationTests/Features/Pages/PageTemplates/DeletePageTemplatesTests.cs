using System.Net;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

public class DeletePageTemplatesTests : BaseWebClientProvider
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
    
    [Test]
    public async Task Delete_WhenEntityExists_ReturnsOk()
    {
        _pagesFixture.InsertPageTemplate();

        HttpResponseMessage response = await Client.DeleteAsync("/api/PageTemplates/1");

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(_pagesFixture.CountPageTemplates(), Is.EqualTo(0));
        });
    }
    
    [Test]
    public async Task Delete_WhenEntityDoesNotExist_ReturnsNotFound()
    {
        _pagesFixture.InsertPageTemplate();

        HttpResponseMessage response = await Client.DeleteAsync("/api/PageTemplates/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}
