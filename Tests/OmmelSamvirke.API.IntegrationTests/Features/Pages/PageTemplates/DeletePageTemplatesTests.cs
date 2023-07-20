using System.Net;
using OmmelSamvirke.API.E2ETests.Common;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.PageTemplates;

public class DeletePageTemplatesTests : BaseWebClientProvider
{
    [Test]
    public async Task Delete_WhenEntityExists_ReturnsOk()
    {
        TestFixtures.InsertPageTemplate();

        HttpResponseMessage response = await Client.DeleteAsync("/api/PageTemplates/1");

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(TestFixtures.CountPageTemplates(), Is.EqualTo(0));
        });
    }
    
    [Test]
    public async Task Delete_WhenEntityDoesNotExist_ReturnsNotFound()
    {
        TestFixtures.InsertPageTemplate();

        HttpResponseMessage response = await Client.DeleteAsync("/api/PageTemplates/2");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
