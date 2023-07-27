using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Pages;

public class PutPagesTests : BaseWebClientProvider
{
    [Test]
    public async Task Update_GivenValidData_ReturnsOk()
    {
        TestFixtures.InsertCommunity();
        Page originalPage = TestFixtures.InsertPage();
        
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        requestDto.Id = 1;
        requestDto.Name = "Update Name";
        string requestBody = JsonConvert.SerializeObject(new {
            OriginalPage = originalPage,
            UpdatedPage = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage responseMessage = await Client.PutAsync("/api/Pages", new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);

        Assert.Multiple(() =>
        {
            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(originalPage.Id));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(requestDto.Name));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)requestDto.State));
        });
    }

    [Test]
    public async Task Update_GivenOriginalPageHasStaleModifiedDate_ReturnsConflict()
    {
        TestFixtures.InsertCommunity();
        Page originalPage = TestFixtures.InsertPage();
        originalPage.DateModified = originalPage.DateModified?.AddSeconds(1);
        
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        requestDto.Id = 1;
        requestDto.Name = "Update Name";
        string requestBody = JsonConvert.SerializeObject(new {
            OriginalPage = originalPage,
            UpdatedPage = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage responseMessage = await Client.PutAsync("/api/Pages", new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }
}
