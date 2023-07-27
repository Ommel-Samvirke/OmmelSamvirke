using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.IntegrationTests.Common;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.IntegrationTests.Features.Pages.Pages;

public class PostPagesTests : BaseWebClientProvider
{
    [Test]
    public async Task Create_WhenPageIsValid_ReturnsPage()
    {
        TestFixtures.InsertCommunity();
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        string requestBody = JsonConvert.SerializeObject(new {
            Page = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage response = await Client.PostAsync("/api/pages", new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(1));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(requestDto.Name));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)requestDto.State));
        });
    }

    [Test]
    public async Task Create_WhenCommunityDoesNotExist_ReturnsNotFound()
    {
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        string requestBody = JsonConvert.SerializeObject(new {
            page = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage response = await Client.PostAsync("/api/pages", new StringContent(
            requestBody,
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
        TestFixtures.InsertCommunity();
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        requestDto.Name = new string('a', nameLength);
        string requestBody = JsonConvert.SerializeObject(new {
            page = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage response = await Client.PostAsync("/api/pages", new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [TestCase(1)]
    [TestCase(100)]
    [TestCase(200)]
    public async Task Create_WhenPageNameIsValid_ReturnsCreated(int nameLength)
    {
        TestFixtures.InsertCommunity();
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        requestDto.Name = new string('a', nameLength);
        string requestBody = JsonConvert.SerializeObject(new {
            page = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage response = await Client.PostAsync("/api/pages", new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test]
    public async Task Create_WhenPageWithNameAlreadyExists_ReturnsBadRequest()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        PageDto requestDto = GlobalPageFixtures.DefaultPageDto();
        string requestBody = JsonConvert.SerializeObject(new {
            page = requestDto
        }, SerializerSettings.JsonSerializerSettings);
        
        HttpResponseMessage response = await Client.PostAsync("/api/pages", new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json"
        ));
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
