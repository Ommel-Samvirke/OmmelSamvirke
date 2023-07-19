using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

[TestFixture]
public class GetPageTemplatesTests : BaseWebClientProvider
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
    public async Task GetById_WhenIdExists_ReturnsPageTemplate()
    {
        _pagesFixture.InsertPageTemplate();
        
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
        _pagesFixture.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates/2");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetByState_WhenStateHasNoPageTemplates_ReturnsEmptyList()
    {
        _pagesFixture.InsertPageTemplates(new List<PageTemplateState>
        {
            PageTemplateState.Archived,
            PageTemplateState.Custom,
            PageTemplateState.Hidden
        });
        
        HttpResponseMessage response = await Client.GetAsync($"/api/PageTemplates?state={(int)PageTemplateState.Public}");
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content.ReadAsStringAsync().Result, Is.EqualTo("[]"));
        });
    }

    [Test]
    public async Task GetByState_WhenStateHasPageTemplates_ReturnsPageTemplates()
    {
        _pagesFixture.InsertPageTemplates(new List<PageTemplateState>
        {
            PageTemplateState.Public,
            PageTemplateState.Public,
            PageTemplateState.Hidden
        });
        
        HttpResponseMessage response = await Client.GetAsync($"/api/PageTemplates?state={(int)PageTemplateState.Public}");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        List<dynamic> deserializedResponse = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse)!;
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content.ReadAsStringAsync().Result, Is.Not.EqualTo("[]"));
            Assert.That(deserializedResponse, Has.Count.EqualTo(2));
        });
    }
    
    [Test]
    public async Task GetByState_WhenStateIsInvalid_ReturnsBadRequest()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates?state=999999999");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}
