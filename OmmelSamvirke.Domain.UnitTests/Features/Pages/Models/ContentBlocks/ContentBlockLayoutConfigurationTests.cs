using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models.ContentBlocks;

[TestFixture]
public class ContentBlockLayoutConfigurationTests : PagesBaseTestModel
{
    private int _defaultX;
    private int _defaultY;
    private int _defaultWidth;
    private int _defaultHeight;
    private ContentBlockLayoutConfiguration _defaultLayoutConfiguration = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _defaultX = 0;
        _defaultY = 0;
        _defaultWidth = 6;
        _defaultHeight = 10;
        _defaultLayoutConfiguration = new ContentBlockLayoutConfiguration(
            _defaultX,
            _defaultY,
            _defaultWidth,
            _defaultHeight
        );
    }
    
    [Test]
    public void Can_Create_LayoutConfiguration_With_Valid_Data()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_defaultLayoutConfiguration.XPosition, Is.EqualTo(_defaultX));
            Assert.That(_defaultLayoutConfiguration.YPosition, Is.EqualTo(_defaultY));
            Assert.That(_defaultLayoutConfiguration.Width, Is.EqualTo(_defaultWidth));
            Assert.That(_defaultLayoutConfiguration.Height, Is.EqualTo(_defaultHeight));
        });
    }

    [Test]
    public void Can_Create_HeadlineBlock_With_Id_And_Valid_Data()
    {
        const int id = 1;
        DateTime now = DateTime.Now;

        ContentBlockLayoutConfiguration layoutConfiguration = new(
            id,
            now,
            now,
            _defaultX,
            _defaultY,
            _defaultWidth,
            _defaultHeight
        );

        Assert.Multiple(() =>
        {
            Assert.That(layoutConfiguration.Id, Is.EqualTo(id));
            Assert.That(layoutConfiguration.XPosition, Is.EqualTo(_defaultX));
            Assert.That(layoutConfiguration.YPosition, Is.EqualTo(_defaultY));
            Assert.That(layoutConfiguration.Width, Is.EqualTo(_defaultWidth));
            Assert.That(layoutConfiguration.Height, Is.EqualTo(_defaultHeight));
            Assert.That(layoutConfiguration.DateCreated, Is.EqualTo(now));
            Assert.That(layoutConfiguration.DateModified, Is.EqualTo(now));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_XPosition_Is_Negative()
    {
        Assert.That(() => new ContentBlockLayoutConfiguration(
            -1,
            _defaultY,
            _defaultWidth,
            _defaultHeight
        ), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_YPosition_Is_Negative()
    {
        Assert.That(() => new ContentBlockLayoutConfiguration(
            _defaultX,
            -1,
            _defaultWidth,
            _defaultHeight
        ), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Width_Is_Not_Positive()
    {
        Assert.That(() => new ContentBlockLayoutConfiguration(
            _defaultX,
            _defaultY,
            -1,
            _defaultHeight
        ), Throws.ArgumentException);
    }
    
    [Test]
    public void Should_Throw_Exception_When_Height_Is_Not_Positive()
    {
        Assert.That(() => new ContentBlockLayoutConfiguration(
            _defaultX,
            _defaultY,
            _defaultWidth,
            -1
        ), Throws.ArgumentException);
    }
}
