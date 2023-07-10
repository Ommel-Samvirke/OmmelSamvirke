using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models;

[TestFixture]
public class PageTemplateTests
{
    [Test]
    public void Can_Create_PageTemplate_With_Valid_Data()
    {
        const string name = "test_template";
        PageTemplate pageTemplate = new(name, new List<ContentBlock>(), PageTemplateState.Public);

        Assert.Multiple(() =>
        {
            Assert.That(pageTemplate.Name, Is.EqualTo(name));
            Assert.That(pageTemplate.ContentBlocks, Is.Empty);
            Assert.That(pageTemplate.State, Is.EqualTo(PageTemplateState.Public));
        });
    }

    [Test]
    public void Can_Create_PageTemplate_With_Id_And_Valid_Data()
    {
        const int id = 1;
        const string name = "test_template";
        DateTime dateCreated = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        PageTemplate pageTemplate = new(id, dateCreated, dateModified, name, new List<ContentBlock>(), PageTemplateState.Public);

        Assert.Multiple(() =>
        {
            Assert.That(pageTemplate.Id, Is.EqualTo(id));
            Assert.That(pageTemplate.Name, Is.EqualTo(name));
            Assert.That(pageTemplate.ContentBlocks, Is.Empty);
            Assert.That(pageTemplate.State, Is.EqualTo(PageTemplateState.Public));
            Assert.That(pageTemplate.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(pageTemplate.DateModified, Is.EqualTo(dateModified));
        });
    }

    [Test]
    public void Should_Throw_Exception_When_Name_Is_Empty()
    {
        const string name = "";

        Assert.That(() => new PageTemplate(name, new List<ContentBlock>(), PageTemplateState.Public), Throws.ArgumentException);
    }

    [Test]
    public void Should_Throw_Exception_When_Name_Is_Too_Long()
    {
        string name = new('a', 101);

        Assert.That(() => new PageTemplate(name, new List<ContentBlock>(), PageTemplateState.Public), Throws.ArgumentException);
    }
}
