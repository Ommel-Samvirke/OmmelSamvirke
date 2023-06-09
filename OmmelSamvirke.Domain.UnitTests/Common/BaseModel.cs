using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.UnitTests.Common;

/// <summary>
/// Since BaseModel is an abstract class, the tests will
/// performed on <see cref="NewsletterSubscriber"/> which
/// inherits from BaseClass.
/// </summary>
public class BaseModel
{
    private const string TestEmail = "testmail@example.com";
    
    [Test]
    public void DefaultConstructor_InitializesProperties()
    {
        NewsletterSubscriber baseModel = new(TestEmail);
        
        Assert.Multiple(() =>
        {
            Assert.That(baseModel.Id, Is.Null);
            Assert.That(baseModel.DateCreated, Is.InRange(
                DateTime.UtcNow.AddSeconds(-1),
                DateTime.UtcNow.AddSeconds(1)
            ));
            Assert.That(baseModel.DateModified, Is.InRange(
                DateTime.UtcNow.AddSeconds(-1),
                DateTime.UtcNow.AddSeconds(1)
            ));
        });
    }

    [Test]
    public void ParameterConstructor_InitializesProperties()
    {
        DateTime testDateTime = DateTime.UtcNow;
        NewsletterSubscriber baseModel = new(1, testDateTime, testDateTime, TestEmail);
        
        Assert.Multiple(() =>
        {
            Assert.That(baseModel.Id, Is.EqualTo(1));
            Assert.That(baseModel.DateCreated, Is.EqualTo(testDateTime));
            Assert.That(baseModel.DateModified, Is.EqualTo(testDateTime));
        });
    }

    [Test]
    public void Equals_GivenEqualModel_ReturnsTrue()
    {
        NewsletterSubscriber model1 = new(TestEmail);

        bool equalsResult = model1.Equals(model1);
        
        Assert.That(equalsResult, Is.EqualTo(true));
    }

    [Test]
    public void Equals_GivenDifferentModel_ReturnsFalse()
    {
        NewsletterSubscriber model1 = new(TestEmail);
        NewsletterSubscriber model2 = new(TestEmail);

        bool equalsResult = model1.Equals(model2);
        
        Assert.That(equalsResult, Is.EqualTo(false));
    }
    
    [Test]
    public void Equals_GivenEqualObject_ReturnsTrue()
    {
        NewsletterSubscriber model1 = new(TestEmail);

        bool equalsResult = model1.Equals((object)model1);
        
        Assert.That(equalsResult, Is.EqualTo(true));
    }
    
    [Test]
    public void Equals_GivenDifferentObject_ReturnsFalse()
    {
        NewsletterSubscriber model1 = new(TestEmail);
        object model2 = new();

        bool equalsResult = model1.Equals(model2);
        
        Assert.That(equalsResult, Is.EqualTo(false));
    }
}
