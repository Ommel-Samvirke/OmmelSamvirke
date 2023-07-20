using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

public class PostPageTemplatesTests : BaseWebClientProvider
{
    [Test]
    public async Task CreatePageTemplates_WhenPageTemplateIsValid_ReturnsPageTemplate()
    {
        HttpResponseMessage response = await Client.PostAsync("/api/PageTemplates", new StringContent(
            @"{
                ""pageTemplateCreateDto"": {
                    ""name"": ""TestTemplateInMemory2""
                }
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
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo("TestTemplateInMemory2"));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)PageTemplateState.Hidden));
        });
    }
    
    [TestCase(0)]
    [TestCase(226)]
    [TestCase(1000)]
    public async Task CreatePageTemplates_WhenPageTemplateNameIsInvalid_ReturnsBadRequest(int nameLength)
    {
        HttpResponseMessage response = await Client.PostAsync("/api/PageTemplates", new StringContent(
            $@"{{
                ""pageTemplateCreateDto"": {{
                    ""name"": ""{new string('a', nameLength)}""
                }}
            }}",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        });
    }
    
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(225)]
    public async Task CreatePageTemplates_WhenPageTemplateNameValid_ReturnsBadRequest(int nameLength)
    {
        HttpResponseMessage response = await Client.PostAsync("/api/PageTemplates", new StringContent(
            $@"{{
                ""pageTemplateCreateDto"": {{
                    ""name"": ""{new string('a', nameLength)}""
                }}
            }}",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        });
    }
    
    [Test]
    public async Task CreatePageTemplates_WhenPageTemplateAlreadyExists_ReturnsBadRequest()
    {
        TestFixtures.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.PostAsync("/api/PageTemplates", new StringContent(
            $@"{{
                ""pageTemplateCreateDto"": {{
                    ""name"": ""{GlobalPageTemplatesFixtures.DefaultPageTemplate().Name}""
                }}
            }}",
            Encoding.UTF8,
            "application/json"
        ));

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        });
    }
    
    // TODO - Add test for when a Page Template is created from a Page
}
