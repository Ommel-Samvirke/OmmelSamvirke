using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmmelSamvirke.API.E2ETests.Common;
using OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Pages;

public class GetPagesTests : BaseWebClientProvider
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
    public async Task GetById_WhenIdExists_ReturnsPage()
    {
        Page page = _pagesFixture.InsertPage();

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
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetContentBlockData_WhenIdExists_ReturnsListOfContentBlocks()
    {
        _pagesFixture.InsertPage();
        _pagesFixture.InsertContentBlockData();
        _pagesFixture.InsertContentBlockData();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/ContentBlockData/1");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        List<dynamic> deserializedResponse = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse)!;
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deserializedResponse, Has.Count.EqualTo(2));
        });
    }
    
    [Test]
    public async Task GetContentBlockData_WhenNoContentBlockDataIsAssociated_ReturnsEmptyList()
    {
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/ContentBlockData/1");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        List<dynamic> deserializedResponse = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse)!;
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deserializedResponse, Has.Count.EqualTo(0));
        });
    }
    
    [Test]
    public async Task GetContentBlockData_WhenIdDoesNotExist_ReturnsNotFound()
    {
        _pagesFixture.InsertPage();
        _pagesFixture.InsertContentBlockData();
        _pagesFixture.InsertContentBlockData();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/ContentBlockData/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetNextPage_WhenDataValid_ReturnsNextPage()
    {
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        Page page2 = _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

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
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetNext?CommunityId=1&CurrentPageId=1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetNextPage_WhenPageDoesNotExist_ReturnsNotFound()
    {
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetNext?CommunityId=1&CurrentPageId=4");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetNextPage_WhenAtLastPage_ReturnsFirstPage()
    {
        _pagesFixture.InsertCommunity();
        Page page1 = _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

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
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        Page page2 = _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

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
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetPrevious?CommunityId=1&CurrentPageId=1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetPreviousPage_WhenPageDoesNotExist_ReturnsNotFound()
    {
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages/GetPrevious?CommunityId=1&CurrentPageId=4");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetPreviousPage_WhenAtFirstPage_ReturnsLastPage()
    {
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        Page page3 = _pagesFixture.InsertPage();

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
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

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
        _pagesFixture.InsertCommunity();

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
        _pagesFixture.InsertCommunity();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();
        _pagesFixture.InsertPage();

        HttpResponseMessage response = await Client.GetAsync("/api/Pages?CommunityId=2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
}
