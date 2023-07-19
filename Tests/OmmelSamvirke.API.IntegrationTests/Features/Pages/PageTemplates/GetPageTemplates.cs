using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

[TestFixture]
public class GetPageTemplates : BaseWebClientProvider
{
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        using IServiceScope scope = Factory.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        PagesFixture _ = new(dbContext);
    }
    
    [Test]
    public async Task GetById_WhenIdExists_ReturnsPageTemplate()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates/1");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(1));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(GlobalPagesFixtures.DefaultPageTemplate().Name));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)GlobalPagesFixtures.DefaultPageTemplate().State));
        });
    }
    
    [Test]
    public async Task GetById_WhenIdDoesNotExist_ReturnNotFound()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates/2");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}
