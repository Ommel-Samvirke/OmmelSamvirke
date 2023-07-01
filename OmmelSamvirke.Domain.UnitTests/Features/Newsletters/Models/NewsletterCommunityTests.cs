using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Newsletters.Models;

[TestFixture]
public class NewsletterCommunityTests
{
    private string _defaultName = null!;

    [SetUp]
    public void SetUp()
    {
        _defaultName = "TestName";
    }

    [Test]
    public void DefaultConstructor_InitializesProperties()
    {
        NewsletterCommunity newsletterCommunity = new(_defaultName);
        
        Assert.That(newsletterCommunity.Name, Is.EqualTo(_defaultName));
    }
    
    [Test]
    public void BaseConstructor_InitializesProperties()
    {
        DateTime testTimestamp = DateTime.UtcNow;
        
        NewsletterCommunity newsletterCommunity = new(
            1,
            testTimestamp,
            testTimestamp,
            _defaultName
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterCommunity.Id, Is.EqualTo(1));
            Assert.That(newsletterCommunity.DateCreated, Is.EqualTo(testTimestamp));
            Assert.That(newsletterCommunity.DateModified, Is.EqualTo(testTimestamp));
            Assert.That(newsletterCommunity.Name, Is.EqualTo(_defaultName));
        });
    }

    [TestCase("")]
    [TestCase("a")]
    [TestCase("ab")]
    public void Constructor_GivenTooShortName_ThrowsArgumentException(string name)
    {
        Assert.That(() => new NewsletterCommunity(name), Throws.ArgumentException);
    }

    [Test]
    public void Constructor_GivenTooLongName_ThrowsArgumentException()
    {
        string testName = new('a', 36);
        Assert.That(() => new NewsletterCommunity(testName), Throws.ArgumentException);
    }

    [Test]
    public void Name_SetName_UpdatesName()
    {
        NewsletterCommunity newsletterCommunity = new(_defaultName);
        const string newName = "NewName";

        newsletterCommunity.Name = newName;
        
        Assert.That(newsletterCommunity.Name, Is.EqualTo(newName));
    }
}
