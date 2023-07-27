using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Pages;

public class GetPagesTests : BaseWebClientProvider
{
    [Test]
    public async Task GetById_WhenIdExists_ReturnsPage()
    {
        Page page = TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/1");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["id"]!.Value<int>(), Is.EqualTo(1));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(page.Name));
            Assert.That(jsonResponseBody["state"]!.Value<int>(), Is.EqualTo((int)page.State));
        });
    }
    
    [Test]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetContentBlockData_WhenIdExists_ReturnsListOfContentBlocks()
    {
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/ContentBlocks?layoutConfigurationId=1");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        List<dynamic> deserializedResponse = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse)!;
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deserializedResponse, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public async Task GetContentBlockData_WhenIdDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/ContentBlockData/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetNextPage_WhenDataValid_ReturnsNextPage()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        Page page2 = TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetNext?CommunityId=1&CurrentPageId=1");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(page2.Name));
        });
    }
    
    [Test]
    public async Task GetNextPage_WhenCommunityDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetNext?CommunityId=1&CurrentPageId=1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetNextPage_WhenPageDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetNext?CommunityId=1&CurrentPageId=4");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetNextPage_WhenAtLastPage_ReturnsFirstPage()
    {
        TestFixtures.InsertCommunity();
        Page page1 = TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetNext?CommunityId=1&CurrentPageId=3");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(page1.Name));
        });
    }
    
    [Test]
    public async Task GetPreviousPage_WhenDataValid_ReturnsPreviousPage()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        Page page2 = TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetPrevious?CommunityId=1&CurrentPageId=3");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(page2.Name));
        });
    }
    
    [Test]
    public async Task GetPreviousPage_WhenCommunityDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetPrevious?CommunityId=1&CurrentPageId=1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetPreviousPage_WhenPageDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetPrevious?CommunityId=1&CurrentPageId=4");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetPreviousPage_WhenAtFirstPage_ReturnsLastPage()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        Page page3 = TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetPrevious?CommunityId=1&CurrentPageId=1");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponseBody = JObject.Parse(responseBody);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonResponseBody["name"]!.Value<string>(), Is.EqualTo(page3.Name));
        });
    }
    
    [Test]
    public async Task GetByCommunityId_WhenIdExists_ReturnsListOfPages()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages?CommunityId=1");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        List<dynamic> deserializedResponse = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse)!;
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deserializedResponse, Has.Count.EqualTo(3));
        });
    }
    
    [Test]
    public async Task GetByCommunityId_WhenIdExistsButNoPagesAreLinked_ReturnsEmptyList()
    {
        TestFixtures.InsertCommunity();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages?CommunityId=1");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        List<dynamic> deserializedResponse = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse)!;
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deserializedResponse, Has.Count.EqualTo(0));
        });
    }
    
    [Test]
    public async Task GetByCommunityId_WhenCommunityDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertCommunity();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages?CommunityId=2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
