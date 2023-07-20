using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

public class PutPageTemplatesTests : BaseWebClientProvider
{
    private static PagesFixture _pagesFixture = null!;
    private readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        using IServiceScope scope = Factory.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _pagesFixture = new PagesFixture(dbContext);
    }
    
    [Test]
    public async Task MakePublic_GivenValidId_ReturnsOk()
    {
        _pagesFixture.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/MakePublic/1", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task Archive_GivenValidId_ReturnsOk()
    {
        _pagesFixture.InsertPageTemplate();
        
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/Archive/1", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task MakePublic_GivenInvalidId_ReturnsNotFound()
    {
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/MakePublic/1", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task Archive_GivenInvalidId_ReturnsNotFound()
    {
        HttpResponseMessage response = await Client.PutAsync("/api/PageTemplates/Archive/1", null!);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_GivenValidData_ReturnsOk()
    {
        PageTemplate originalPageTemplate = _pagesFixture.InsertPageTemplate();
        ContentBlock contentBlock = GlobalContentBlockFixtures.DefaultContentBlock();

        string updatedContentBlocks = JsonConvert.SerializeObject(new List<ContentBlock>
        {
            contentBlock
        }, _jsonSerializerSettings);

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
        PageTemplate originalPageTemplate = _pagesFixture.InsertPageTemplate(pageTemplate);
        ContentBlock contentBlock = GlobalContentBlockFixtures.DefaultContentBlock();

        string updatedContentBlocks = JsonConvert.SerializeObject(new List<ContentBlock>
        {
            contentBlock
        }, _jsonSerializerSettings);

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
    
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}
