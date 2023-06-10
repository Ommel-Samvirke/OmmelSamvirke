using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Newsletters.Models;

public class NewsletterCommentTests
{
    private int _defaultModelId;
    private DateTime _defaultDateCreated;
    private DateTime _defaultDateModified;
    private int _defaultUserId;
    private int _defaultNewsletterId;
    private string _defaultContent = null!;

    [SetUp]
    public void SetUp()
    {
        _defaultModelId = 1;
        _defaultDateCreated = DateTime.UtcNow;
        _defaultDateModified = _defaultDateCreated;
        _defaultUserId = 2;
        _defaultNewsletterId = 3;
        _defaultContent = "This is a test comment";
    }

    [Test]
    public void DefaultConstructor_InitializesParameters()
    {
        NewsletterComment newsletterComment = new(_defaultUserId, _defaultNewsletterId, _defaultContent);
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterComment.UserId, Is.EqualTo(_defaultUserId));
            Assert.That(newsletterComment.NewsletterId, Is.EqualTo(_defaultNewsletterId));
            Assert.That(newsletterComment.Content, Is.EqualTo(_defaultContent));
        });
    }

    [Test]
    public void BaseConstructor_InitializesParameters()
    {
        NewsletterComment newsletterComment = new(
            _defaultModelId,
            _defaultDateCreated,
            _defaultDateModified,
            _defaultUserId,
            _defaultNewsletterId,
            _defaultContent
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(newsletterComment.Id, Is.EqualTo(_defaultModelId));
            Assert.That(newsletterComment.DateCreated, Is.EqualTo(_defaultDateCreated));
            Assert.That(newsletterComment.DateModified, Is.EqualTo(_defaultDateModified));
            Assert.That(newsletterComment.UserId, Is.EqualTo(_defaultUserId));
            Assert.That(newsletterComment.NewsletterId, Is.EqualTo(_defaultNewsletterId));
            Assert.That(newsletterComment.Content, Is.EqualTo(_defaultContent));
        });
    }

    [Test]
    public void Content_SetContent_UpdatesContent()
    {
        NewsletterComment newsletterComment = new(_defaultUserId, _defaultNewsletterId, _defaultContent);
        const string updatedComment = "This is an updated comment";

        newsletterComment.Content = updatedComment;
        
        Assert.That(newsletterComment.Content, Is.EqualTo(updatedComment));
    }
}