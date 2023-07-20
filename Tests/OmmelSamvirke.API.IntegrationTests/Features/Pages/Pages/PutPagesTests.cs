using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Pages;

public class PutPagesTests : BaseWebClientProvider
{
    [Test]
    public async Task Update_GivenValidData_ReturnsOk()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPageTemplate();
        Page originalPage = TestFixtures.InsertPage();
        HeadlineBlockData headlineBlockData = (HeadlineBlockData)TestFixtures.InsertContentBlockData().First();
        headlineBlockData.Headline = "Updated headline";

        string requestBody =
            $@"{{
                ""originalPage"": {{
                    ""name"": ""{originalPage.Name}"",
                    ""state"": {(int)originalPage.State},
                    ""id"": {originalPage.Id},
                    ""dateCreated"": ""{originalPage.DateCreated:yyyy-MM-ddTHH:mm:ss.ffffff}"",
                    ""dateModified"": ""{originalPage.DateModified:yyyy-MM-ddTHH:mm:ss.ffffff}""
                }},
                ""updatedPage"": {{
                    ""name"": ""Updated name"",
                    ""state"": {(int)PageState.Visible},
                    ""id"": {originalPage.Id},
                    ""pageTemplateId"": 1
                }},
                ""updatedContentBlockDataElements"": [
                    {{
                        ""id"": 1,
                        ""baseContentBlockId"": 1,
                        ""pageId"": 1,
                        ""headline"": ""Updated Headline"",
                        ""contentBlockType"": 0
                    }}
                ]
            }}";

        HttpResponseMessage responseMessage = await Client.PutAsync("/api/Pages", 
            new StringContent(requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);

        Assert.Multiple(() =>
        {
            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(originalPage.Id));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo("Updated name"));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)PageState.Visible));
        });
    }
    
    [Test]
    public async Task Update_GivenOriginalPageHasStaleModifiedDate_ReturnsConflict()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPageTemplate();
        Page originalPage = TestFixtures.InsertPage();
        HeadlineBlockData headlineBlockData = (HeadlineBlockData)TestFixtures.InsertContentBlockData().First();
        headlineBlockData.Headline = "Updated headline";

        string requestBody =
            $@"{{
                ""originalPage"": {{
                    ""name"": ""{originalPage.Name}"",
                    ""state"": {(int)originalPage.State},
                    ""id"": {originalPage.Id},
                    ""dateCreated"": ""{originalPage.DateCreated:yyyy-MM-ddTHH:mm:ss.ffffff}"",
                    ""dateModified"": ""2023-07-20T17:27:38.123456""
                }},
                ""updatedPage"": {{
                    ""name"": ""Updated name"",
                    ""state"": {(int)PageState.Visible},
                    ""id"": {originalPage.Id},
                    ""pageTemplateId"": 1
                }},
                ""updatedContentBlockDataElements"": [
                    {{
                        ""id"": 1,
                        ""baseContentBlockId"": 1,
                        ""pageId"": 1,
                        ""headline"": ""Updated Headline"",
                        ""contentBlockType"": 0
                    }}
                ]
            }}";

        HttpResponseMessage responseMessage = await Client.PutAsync("/api/Pages", 
            new StringContent(requestBody,
            Encoding.UTF8,
            "application/json"
        ));

        Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }
}
