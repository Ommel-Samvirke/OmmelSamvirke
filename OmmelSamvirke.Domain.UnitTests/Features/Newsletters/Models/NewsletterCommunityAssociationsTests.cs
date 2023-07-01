using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Newsletters.Models;

[TestFixture]
public class NewsletterCommunityAssociationsTests
{
    private ISet<int> _defaultNewsletterCommunities = null!;

    [SetUp]
    public void SetUp()
    {
        _defaultNewsletterCommunities = new HashSet<int>()
        {
            1,
            2
        };
    }

    [Test]
    public void DefaultConstructorWithoutNewsletterCommunities_InitializesProperties()
    {
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(1);
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterCommunityAssociations.NewsletterId, Is.EqualTo(1));
            Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(0));
        });    
    }
    
    [Test]
    public void DefaultConstructorWithNewsletterCommunities_InitializesProperties()
    {
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(1, _defaultNewsletterCommunities);
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterCommunityAssociations.NewsletterId, Is.EqualTo(1));
            Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public void BaseConstructorWithoutNewsletterCommunities_InitializesProperties()
    {
        DateTime testTimestamp = DateTime.UtcNow;
        
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(
            1,
            testTimestamp,
            testTimestamp,
            1
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterCommunityAssociations.Id, Is.EqualTo(1));
            Assert.That(newsletterCommunityAssociations.DateCreated, Is.EqualTo(testTimestamp));
            Assert.That(newsletterCommunityAssociations.DateModified, Is.EqualTo(testTimestamp));
            Assert.That(newsletterCommunityAssociations.NewsletterId, Is.EqualTo(1));
            Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(0));
        });
    }
    
    [Test]
    public void BaseConstructorWithNewsletterCommunities_InitializesProperties()
    {
        DateTime testTimestamp = DateTime.UtcNow;
        
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(
            1,
            testTimestamp,
            testTimestamp,
            1,
            _defaultNewsletterCommunities
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterCommunityAssociations.Id, Is.EqualTo(1));
            Assert.That(newsletterCommunityAssociations.DateCreated, Is.EqualTo(testTimestamp));
            Assert.That(newsletterCommunityAssociations.DateModified, Is.EqualTo(testTimestamp));
            Assert.That(newsletterCommunityAssociations.NewsletterId, Is.EqualTo(1));
            Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public void NewsletterCommunities_AddingNewsletterCommunity_IncreasesCount()
    {
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(1, _defaultNewsletterCommunities);

        newsletterCommunityAssociations.NewsletterCommunities.Add(3);
        
        Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(3));
    }
    
    [Test]
    public void NewsletterCommunities_AddingNewsletterCommunityWithExistingId_MaintainsCount()
    {
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(1, _defaultNewsletterCommunities);

        newsletterCommunityAssociations.NewsletterCommunities.Add(2);
        
        Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(2));
    }
    
    [Test]
    public void NewsletterCommunities_SettingNewsletterCommunities_ReplacesSet()
    {
        NewsletterCommunityAssociations newsletterCommunityAssociations = new(1, _defaultNewsletterCommunities);
        ISet<int> newNewsletterCommunities = new HashSet<int>() { 1, 2, 3, 4 };

        newsletterCommunityAssociations.NewsletterCommunities = newNewsletterCommunities;
        
        Assert.That(newsletterCommunityAssociations.NewsletterCommunities, Has.Count.EqualTo(4));
    }
}
