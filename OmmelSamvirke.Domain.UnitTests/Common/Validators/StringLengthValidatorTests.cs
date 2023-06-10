using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.UnitTests.Common.Validators;

public class StringLengthValidatorTests
{
    private string _defaultInput = null!;

    [SetUp]
    public void SetUp()
    {
        _defaultInput = new string('a', 5);
    }
    
    [TestCase(6)]
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(int.MaxValue)]
    public void Validate_GivenTooLowInteger_ThrowArgumentException(int minLength)
    {
        Assert.That(() => StringLengthValidator.Validate(_defaultInput, minLength), Throws.ArgumentException);
    }

    [TestCase(4)]
    [TestCase(1)]
    [TestCase(0)]
    [TestCase(-10)]
    [TestCase(int.MinValue + 1)]
    public void Validate_GivenTooHighInteger_ThrowArgumentException(int maxLength)
    {
        Assert.That(() => StringLengthValidator.Validate(_defaultInput, int.MinValue, maxLength), Throws.ArgumentException);
    }

    [TestCase(0, 10)]
    [TestCase(4, 6)]
    [TestCase(5, 6)]
    [TestCase(-5, 10)]
    public void Validate_GivenValidInteger_ThrowNothing(int minLength, int maxLength)
    {
        Assert.That(() => StringLengthValidator.Validate(_defaultInput, minLength, maxLength), Throws.Nothing);
    }

    [Test]
    public void Validate_GivenMinValueAndMaxValueEqual_ThrowArgumentException()
    {
        Assert.That(() => StringLengthValidator.Validate("a", 1, 1), Throws.ArgumentException);
    }
}