using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Newsletters.Models;

[TestFixture]
public class MailingListTests
{
    private ISet<NewsletterSubscriber> _newsletterSubscribers = null!;

    [SetUp]
    public void Setup()
    {
        _newsletterSubscribers = new HashSet<NewsletterSubscriber>()
        {
            new("test@example.com")
        };
    }

    [Test]
    public void DefaultConstructorWithoutSubscribers_InitializesProperties()
    {
        MailingList mailingList = new(1);

        Assert.Multiple(() =>
        {
            Assert.That(mailingList.NewsletterCommunityId, Is.EqualTo(1));
            Assert.That(mailingList.NewsletterSubscribers, Is.Not.Null);
            Assert.That(mailingList.NewsletterSubscribers, Is.Empty);
        });
    }

    [Test]
    public void DefaultConstructorWithSubscribers_InitializesProperties()
    {
        MailingList mailingList = new(1, _newsletterSubscribers);

        Assert.Multiple(() =>
        {
            Assert.That(mailingList.NewsletterCommunityId, Is.EqualTo(1));
            Assert.That(mailingList.NewsletterSubscribers, Is.Not.Null);
            Assert.That(mailingList.NewsletterSubscribers, Has.Count.EqualTo(1));
        });
    }


    [Test]
    public void BaseConstructorWithoutSubscribers_InitializesProperties()
    {
        MailingList mailingList = new(1, DateTime.UtcNow, DateTime.UtcNow, 1);

        Assert.Multiple(() =>
        {
            Assert.That(mailingList.NewsletterCommunityId, Is.EqualTo(1));
            Assert.That(mailingList.NewsletterSubscribers, Is.Not.Null);
            Assert.That(mailingList.NewsletterSubscribers, Is.Empty);
        });
    }

    [Test]
    public void BaseConstructorWithSubscribers_InitializesProperties()
    {
        MailingList mailingList = new(
            1,
            DateTime.UtcNow,
            DateTime.UtcNow,
            1,
            _newsletterSubscribers
        );

        Assert.Multiple(() =>
        {
            Assert.That(mailingList.NewsletterCommunityId, Is.EqualTo(1));
            Assert.That(mailingList.NewsletterSubscribers, Is.Not.Null);
            Assert.That(mailingList.NewsletterSubscribers, Has.Count.EqualTo(1));
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_GivenInvalidCommunityId_ThrowsArgumentException(int communityId)
    {
        Assert.That(() => new MailingList(communityId), Throws.ArgumentException);
    }

    [Test]
    public void NewsletterSubscriber_AddingNewsletterSubscriber_IncreasesCount()
    {
        MailingList mailingList = new(1);

        mailingList.NewsletterSubscribers.Add(new NewsletterSubscriber("test1@example.com"));
        mailingList.NewsletterSubscribers.Add(new NewsletterSubscriber("test2@example.com"));

        Assert.That(mailingList.NewsletterSubscribers, Has.Count.EqualTo(2));
    }

    [Test]
    public void NewsletterSubscriber_SetNewsletterSubscribers_ReplacesList()
    {
        MailingList mailingList = new(1, _newsletterSubscribers);

        HashSet<NewsletterSubscriber> newNewsletterSubscribers = new();
        mailingList.NewsletterSubscribers = newNewsletterSubscribers;

        Assert.That(mailingList.NewsletterSubscribers, Is.EqualTo(newNewsletterSubscribers));
    }
}
