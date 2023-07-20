using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

[TestFixture]
public class GetPageTemplatesTests : BaseWebClientProvider
{
    [Test]
    public async Task GetById_WhenIdExists_ReturnsPageTemplate()
    {
        TestFixtures.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates/1");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(1));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(GlobalPageTemplatesFixtures.DefaultPageTemplate().Name));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)GlobalPageTemplatesFixtures.DefaultPageTemplate().State));
        });
    }
    
    [Test]
    public async Task GetById_WhenIdDoesNotExist_ReturnNotFound()
    {
        TestFixtures.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.GetAsync("/api/PageTemplates/2");
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetByState_WhenStateHasNoPageTemplates_ReturnsEmptyList()
    {
        TestFixtures.InsertPageTemplates(new List<PageTemplateState>
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
        TestFixtures.InsertPageTemplates(new List<PageTemplateState>
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
}
