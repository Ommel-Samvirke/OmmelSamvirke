using System.Net;
using OmmelSamvirke.API.IntegrationTests.Common;

namespace OmmelSamvirke.API.IntegrationTests.Features.Pages.Pages;

public class DeletePagesTests : BaseWebClientProvider
{
    [Test]
    public async Task Delete_WhenEntityExists_ReturnsOk()
    {
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.DeleteAsync("/api/Pages/1");

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(TestFixtures.CountPages(), Is.EqualTo(0));
        });
    }
    
    [Test]
    public async Task Delete_WhenEntityDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPage();

        HttpResponseMessage response = await Client.DeleteAsync("/api/Pages/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
