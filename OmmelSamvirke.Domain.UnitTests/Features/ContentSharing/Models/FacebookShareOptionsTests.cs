using OmmelSamvirke.Domain.Features.ContentSharing.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.ContentSharing.Models;

[TestFixture]
public class FacebookShareOptionsTests
{
    [Test]
    public void Can_Create_FacebookShareOptions_With_Valid_Data()
    {
        const string title = "Test Title";
        const string summary = "Test Summary";
        const string imageUrl = "https://testurl.com/image.jpg";

        FacebookShareOptions options = new(title, summary, imageUrl);

        Assert.Multiple(() =>
        {
            Assert.That(options.Title, Is.EqualTo(title));
            Assert.That(options.Summary, Is.EqualTo(summary));
            Assert.That(options.ImageUrl, Is.EqualTo(imageUrl));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_Title_Is_Too_Short()
    {
        const string title = "a";
        const string summary = "Valid summary";
        const string imageUrl = "https://valid.url";

        Assert.That(() => new FacebookShareOptions(title, summary, imageUrl), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Summary_Is_Too_Short()
    {
        const string title = "Valid title";
        const string summary = "a";
        const string imageUrl = "https://valid.url";

        Assert.That(() => new FacebookShareOptions(title, summary, imageUrl), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_ImageUrl_Is_Too_Short()
    {
        const string title = "Valid title";
        const string summary = "Valid summary";
        const string imageUrl = "a";

        Assert.That(() => new FacebookShareOptions(title, summary, imageUrl), Throws.ArgumentException);
    }
}
