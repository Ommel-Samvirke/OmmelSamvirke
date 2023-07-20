using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Pages;

public class PostPagesTests : BaseWebClientProvider
{
    [Test]
    public async Task Create_WhenPageIsValid_ReturnsPage()
    {
        TestFixtures.InsertPageTemplate();
        TestFixtures.InsertCommunity();
        
        HttpResponseMessage response = await Client.PostAsync("/api/Pages", new StringContent(
            @"{
                ""pageTemplateId"": 1,
                ""pageName"": ""Example Page"",
                ""communityId"": 1
            }",
            Encoding.UTF8,
            "application/json"
        ));
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(1));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo("Example Page"));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)PageState.Hidden));
        });
    }
    
    [Test]
    public async Task Create_WhenPageTemplateDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertCommunity();
        
        HttpResponseMessage response = await Client.PostAsync("/api/Pages", new StringContent(
            @"{
                ""pageTemplateId"": 1,
                ""pageName"": ""Example Page"",
                ""communityId"": 1
            }",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task Create_WhenCommunityDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.PostAsync("/api/Pages", new StringContent(
            @"{
                ""pageTemplateId"": 1,
                ""pageName"": ""Example Page"",
                ""communityId"": 1
            }",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [TestCase(0)]
    [TestCase(201)]
    [TestCase(1000)]
    public async Task Create_WhenPageNameIsInvalid_ReturnsBadRequest(int nameLength)
    {
        TestFixtures.InsertPageTemplate();
        TestFixtures.InsertCommunity();
        
        HttpResponseMessage response = await Client.PostAsync("/api/Pages", new StringContent(
            $@"{{
                ""pageTemplateId"": 1,
                ""pageName"": ""{new string('a', nameLength)}"",
                ""communityId"": 1
            }}",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(199)]
    public async Task Create_WhenPageNameIsValid_ReturnsCreated(int nameLength)
    {
        TestFixtures.InsertPageTemplate();
        TestFixtures.InsertCommunity();
        
        HttpResponseMessage response = await Client.PostAsync("/api/Pages", new StringContent(
            $@"{{
                ""pageTemplateId"": 1,
                ""pageName"": ""{new string('a', nameLength)}"",
                ""communityId"": 1
            }}",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }
    
    [Test]
    public async Task Create_WhenPageWithNameAlreadyExists_ReturnsBadRequest()
    {
        TestFixtures.InsertPageTemplate();
        TestFixtures.InsertCommunity();
        Page page = TestFixtures.InsertPage();
        
        HttpResponseMessage response = await Client.PostAsync("/api/Pages", new StringContent(
            $@"{{
                ""pageTemplateId"": 1,
                ""pageName"": ""{page.Name}"",
                ""communityId"": 1
            }}",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
