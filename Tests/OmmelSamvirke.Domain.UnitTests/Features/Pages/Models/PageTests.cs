using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models;

[TestFixture]
public class PageTests
{
     private PageTemplate _template = null!;

    [SetUp]
    public void Setup()
    {
        _template = new PageTemplate(
            "page_template",
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
    }

    [Test]
    public void Can_Create_Page_With_Valid_Data()
    {
        const string name = "test_page";
        Page page = new(name, _template);
        
        Assert.Multiple(() =>
        {
            Assert.That(page.Name, Is.EqualTo(name));
            Assert.That(page.Template, Is.EqualTo(_template));
        });
    }

    [Test]
    public void Can_Create_Page_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const string name = "test_page";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        Page page = new(id, dateCreated, dateModified, name, _template);
        
        Assert.Multiple(() =>
        {
            Assert.That(page.Id, Is.EqualTo(id));
            Assert.That(page.Name, Is.EqualTo(name));
            Assert.That(page.Template, Is.EqualTo(_template));
            Assert.That(page.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(page.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_Name_Is_Empty()
    {
        const string name = "";
        
        Assert.That(() => new Page(name, _template), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Name_Is_Too_Long()
    {
        string name = new('a', 101);
        Assert.That(() => new Page(name, _template), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Template_Is_Null()
    {
        const string name = "test_page";
        PageTemplate nullTemplate = null!;

        Assert.That(() => new Page(name, nullTemplate), Throws.ArgumentException);
    }
}