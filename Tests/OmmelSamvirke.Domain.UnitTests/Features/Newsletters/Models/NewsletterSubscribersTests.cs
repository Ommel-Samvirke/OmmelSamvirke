using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Newsletters.Models;

[TestFixture]
public class NewsletterSubscribersTests
{
    private string _defaultEmail = null!;

    [SetUp]
    public void SetUp()
    {
        _defaultEmail = "test@example.com";
    }

    [Test]
    public void DefaultConstructor_InitializesProperties()
    {
        NewsletterSubscriber newsletterSubscriber = new(_defaultEmail);

        Assert.That(newsletterSubscriber.Email, Is.EqualTo(_defaultEmail));
    }
    
    [Test]
    public void BaseConstructor_InitializesProperties()
    {
        DateTime testTimestamp = DateTime.UtcNow;
        
        NewsletterSubscriber newsletterSubscriber = new(
            1,
            testTimestamp,
            testTimestamp,
            _defaultEmail
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterSubscriber.Id, Is.EqualTo(1));
            Assert.That(newsletterSubscriber.DateCreated, Is.EqualTo(testTimestamp));
            Assert.That(newsletterSubscriber.DateModified, Is.EqualTo(testTimestamp));
            Assert.That(newsletterSubscriber.Email, Is.EqualTo(_defaultEmail));
        });
    }

    [Test]
    public void Email_SetEmail_UpdatesEmail()
    {
        NewsletterSubscriber newsletterSubscriber = new(_defaultEmail);
        const string newEmail = "newEmail@example.com";

        newsletterSubscriber.Email = newEmail;
        
        Assert.That(newsletterSubscriber.Email, Is.EqualTo(newEmail));
    }
}