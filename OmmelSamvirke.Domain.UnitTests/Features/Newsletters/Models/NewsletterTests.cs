using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Newsletters.Models;

[TestFixture]
public class NewsletterTests
{
    private int _defaultModelId;
    private DateTime _defaultCreatedDate;
    private DateTime _defaultModifiedDate;
    private string _defaultTitle = null!;
    private string _defaultHtmlContent = null!;
    private string _defaultPlainContent = null!;
    private int _defaultAdminId;
    private DateTime _defaultSentDate;

    [SetUp]
    public void SetUp()
    {
        _defaultModelId = 1;
        _defaultCreatedDate = DateTime.UtcNow;
        _defaultModifiedDate = _defaultCreatedDate;
        _defaultTitle = "TestTitle";
        _defaultHtmlContent = $"<p>{new string('a', 100)}</p>";
        _defaultPlainContent = $"{new string('a', 100)}";
        _defaultAdminId = 1;
        _defaultSentDate = DateTime.UtcNow;
    }
    
    [Test]
    public void DefaultConstructorWithoutSentDate_InitializesProperties()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletter.Title, Is.EqualTo(_defaultTitle));
            Assert.That(newsletter.HtmlContent, Is.EqualTo(_defaultHtmlContent));
            Assert.That(newsletter.PlainContent, Is.EqualTo(_defaultPlainContent));
            Assert.That(newsletter.AdminId, Is.EqualTo(_defaultAdminId));
            Assert.That(newsletter.SentDate, Is.Null);
            Assert.That(newsletter.Likes, Is.EqualTo(0));
        });
    }

    [Test]
    public void DefaultConstructorWithSentDate_InitializesProperties()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletter.Title, Is.EqualTo(_defaultTitle));
            Assert.That(newsletter.HtmlContent, Is.EqualTo(_defaultHtmlContent));
            Assert.That(newsletter.PlainContent, Is.EqualTo(_defaultPlainContent));
            Assert.That(newsletter.AdminId, Is.EqualTo(_defaultAdminId));
            Assert.That(newsletter.SentDate, Is.EqualTo(_defaultSentDate));
            Assert.That(newsletter.Likes, Is.EqualTo(0));
        });
    }

    [Test]
    public void BaseConstructorWithoutSentDate_InitializesProperties()
    {
        Newsletter newsletter = new(
            _defaultModelId,
            _defaultCreatedDate,
            _defaultModifiedDate,
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletter.Id, Is.EqualTo(_defaultModelId));
            Assert.That(newsletter.DateCreated, Is.EqualTo(_defaultCreatedDate));
            Assert.That(newsletter.DateModified, Is.EqualTo(_defaultModifiedDate));
            Assert.That(newsletter.Title, Is.EqualTo(_defaultTitle));
            Assert.That(newsletter.HtmlContent, Is.EqualTo(_defaultHtmlContent));
            Assert.That(newsletter.PlainContent, Is.EqualTo(_defaultPlainContent));
            Assert.That(newsletter.AdminId, Is.EqualTo(_defaultAdminId));
            Assert.That(newsletter.SentDate, Is.Null);
            Assert.That(newsletter.Likes, Is.EqualTo(0));
        });
    }

    [Test]
    public void BaseConstructorWithSentDate_InitializesProperties()
    {
        Newsletter newsletter = new(
            _defaultModelId,
            _defaultCreatedDate,
            _defaultModifiedDate,
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletter.Id, Is.EqualTo(_defaultModelId));
            Assert.That(newsletter.DateCreated, Is.EqualTo(_defaultCreatedDate));
            Assert.That(newsletter.DateModified, Is.EqualTo(_defaultModifiedDate));
            Assert.That(newsletter.Title, Is.EqualTo(_defaultTitle));
            Assert.That(newsletter.HtmlContent, Is.EqualTo(_defaultHtmlContent));
            Assert.That(newsletter.PlainContent, Is.EqualTo(_defaultPlainContent));
            Assert.That(newsletter.AdminId, Is.EqualTo(_defaultAdminId));
            Assert.That(newsletter.SentDate, Is.EqualTo(_defaultSentDate));
            Assert.That(newsletter.Likes, Is.EqualTo(0));
        });
    }

    [TestCase("")]
    [TestCase("a")]
    [TestCase("test")]
    public void Constructor_GivenShortTitle_ThrowsNewArgumentException(string title)
    {
        Assert.That(() => new Newsletter(
            title,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId
        ), Throws.ArgumentException);
    }
    
    [Test]
    public void Constructor_GivenLongTitle_ThrowsNewArgumentException()
    {
        string title = new('a', 201);
        
        Assert.That(() => new Newsletter(
            title,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId
        ), Throws.ArgumentException);
    }
    
    [TestCase("")]
    [TestCase("a")]
    [TestCase("<p>This content is too short</p>")]
    public void Constructor_GivenShortHtmlContent_ThrowsNewArgumentException(string htmlContent)
    {
        Assert.That(() => new Newsletter(
            _defaultTitle,
            htmlContent,
            _defaultPlainContent,
            _defaultAdminId
        ), Throws.ArgumentException);
    }
    
    [Test]
    public void Constructor_GivenLongHtmlContent_ThrowsNewArgumentException()
    {
        string htmlContent = new('a', 15_001);
        
        Assert.That(() => new Newsletter(
            _defaultTitle,
            htmlContent,
            _defaultPlainContent,
            _defaultAdminId
        ), Throws.ArgumentException);
    }
    
    [TestCase("")]
    [TestCase("a")]
    [TestCase("This content is too short")]
    public void Constructor_GivenShortPlainContent_ThrowsNewArgumentException(string plainContent)
    {
        Assert.That(() => new Newsletter(
            _defaultTitle,
            _defaultHtmlContent,
            plainContent,
            _defaultAdminId
        ), Throws.ArgumentException);
    }
    
    [Test]
    public void Constructor_GivenLongPlainContent_ThrowsNewArgumentException()
    {
        string plainContent = new('a', 15_001);
        
        Assert.That(() => new Newsletter(
            _defaultTitle,
            _defaultHtmlContent,
            plainContent,
            _defaultAdminId
        ), Throws.ArgumentException);
    }

    [Test]
    public void Title_SetTitle_UpdatesTitle()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        const string newTitle = "NewTitle";

        newsletter.Title = newTitle;
        
        Assert.That(newsletter.Title, Is.EqualTo(newTitle));
    }
    
    [Test]
    public void HtmlContent_SetHtmlContent_UpdateHtmlContent()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        string newHtmlContent = $"<p>{new string('a', 50)}</p>";

        newsletter.HtmlContent = newHtmlContent;
        
        Assert.That(newsletter.HtmlContent, Is.EqualTo(newHtmlContent));
    }
    
    [Test]
    public void PlainContent_SetPlainContent_UpdatePlainContent()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        string newPlainContent = new string('a', 50);

        newsletter.PlainContent = newPlainContent;
        
        Assert.That(newsletter.PlainContent, Is.EqualTo(newPlainContent));
    }
    
    [Test]
    public void AdminId_SetAdminId_UpdateAdminId()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        const int newAdminId = 2;

        newsletter.AdminId = newAdminId;
        
        Assert.That(newsletter.AdminId, Is.EqualTo(newAdminId));
    }
    
    [Test]
    public void SentDate_SetSentDate_UpdateSentDate()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        DateTime newSentDate = DateTime.UtcNow;

        newsletter.SentDate = newSentDate;
        
        Assert.That(newsletter.SentDate, Is.EqualTo(newSentDate));
    }
    
    [Test]
    public void SentDate_SetLikes_UpdateLikes()
    {
        Newsletter newsletter = new(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate
        );
        const int newLikes = 1;

        newsletter.Likes = newLikes;
        
        Assert.That(newsletter.Likes, Is.EqualTo(newLikes));
    }
    
    [TestCase(-1)]
    [TestCase(-10)]
    [TestCase(-1000)]
    public void SentDate_SetLikesNegative_ThrowsArgumentException(int likes)
    {
        Assert.That(() => new Newsletter(
            _defaultTitle,
            _defaultHtmlContent,
            _defaultPlainContent,
            _defaultAdminId,
            _defaultSentDate,
            likes
        ), Throws.ArgumentException);
    }
}
