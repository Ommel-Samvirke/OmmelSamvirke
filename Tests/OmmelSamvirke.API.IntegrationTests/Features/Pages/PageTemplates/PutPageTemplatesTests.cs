using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

public class PutPageTemplatesTests : BaseWebClientProvider
{
    [Test]
    public async Task MakePublic_GivenValidId_ReturnsOk()
    {
        TestFixtures.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/1/MakePublic", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task Archive_GivenValidId_ReturnsOk()
    {
        TestFixtures.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/1/Archive", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task MakePublic_GivenInvalidId_ReturnsNotFound()
    {
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/1/MakePublic", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task Archive_GivenInvalidId_ReturnsNotFound()
    {
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/1/Archive", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_GivenValidData_ReturnsOk()
    {
        PageTemplate originalPageTemplate = TestFixtures.InsertPageTemplate();
        ContentBlock contentBlock = GlobalContentBlockFixtures.DefaultContentBlock();

        string updatedContentBlocks = JsonConvert.SerializeObject(new List<ContentBlock>
        {
            contentBlock
        }, SerializerSettings.JsonSerializerSettings);

        string requestBody =
            $@"{{
                ""originalPageTemplate"": {{
                    ""name"": ""{originalPageTemplate.Name}"",
                    ""contentBlocks"": [],
                    ""state"": {(int)originalPageTemplate.State},
                    ""id"": {originalPageTemplate.Id},
                    ""dateCreated"": ""{originalPageTemplate.DateCreated:yyyy-MM-ddTHH:mm:ss.ffffff}"",
                    ""dateModified"": ""{originalPageTemplate.DateModified:yyyy-MM-ddTHH:mm:ss.ffffff}""
                }},
                ""updatedPageTemplate"": {{
                    ""name"": ""Updated name"",
                    ""contentBlocks"": {updatedContentBlocks},
                    ""state"": {(int)PageTemplateState.Archived},
                    ""id"": {originalPageTemplate.Id},
                    ""dateCreated"": null,
                    ""dateModified"": null
                }}
            }}";

        HttpResponseMessage responseMessage = await Client.PutAsync("/api/PageTemplates", 
            new StringContent(requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);

        Assert.Multiple(() =>
        {
            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo("Updated name"));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)PageTemplateState.Archived));
        });
    }
    
    [Test]
    public async Task Update_GivenOriginalPageTemplateDtoHasStaleModifiedDate_ReturnsConflict()
    {
        PageTemplate pageTemplate = GlobalPageTemplatesFixtures.DefaultPageTemplate();
        pageTemplate.ContentBlocks = new List<ContentBlock>
        {
            GlobalContentBlockFixtures.DefaultContentBlock()
        };
        PageTemplate originalPageTemplate = TestFixtures.InsertPageTemplate(pageTemplate);
        ContentBlock contentBlock = GlobalContentBlockFixtures.DefaultContentBlock();

        string updatedContentBlocks = JsonConvert.SerializeObject(new List<ContentBlock>
        {
            contentBlock
        }, SerializerSettings.JsonSerializerSettings);

        string requestBody =
            $@"{{
                ""originalPageTemplate"": {{
                    ""name"": ""{originalPageTemplate.Name}"",
                    ""contentBlocks"": {updatedContentBlocks},
                    ""state"": {(int)originalPageTemplate.State},
                    ""id"": {originalPageTemplate.Id},
                    ""dateCreated"": ""{originalPageTemplate.DateCreated:yyyy-MM-ddTHH:mm:ss.ffffff}"",
                    ""dateModified"": ""{"2023-07-19T23:19:32.123456"}""
                }},
                ""updatedPageTemplate"": {{
                    ""name"": ""Updated name"",
                    ""contentBlocks"": {updatedContentBlocks},
                    ""state"": {(int)originalPageTemplate.State},
                    ""id"": {originalPageTemplate.Id},
                    ""dateCreated"": null,
                    ""dateModified"": null
                }}
            }}";

        HttpResponseMessage responseMessage = await Client.PutAsync("/api/PageTemplates", 
            new StringContent(requestBody,
                Encoding.UTF8,
                "application/json"
            ));

        Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }
}
