using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.UnitTests.Common.Validators;

[TestFixture]
public class IntegerValidatorTests
{
    private int _defaultInput;

    [SetUp]
    public void SetUp()
    {
        _defaultInput = 5;
    }
    
    [TestCase(6)]
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(int.MaxValue)]
    public void Validate_GivenTooLowInteger_ThrowArgumentException(int minValue)
    {
        Assert.That(() => IntegerValidator.Validate(_defaultInput, minValue), Throws.ArgumentException);
    }

    [TestCase(4)]
    [TestCase(1)]
    [TestCase(0)]
    [TestCase(-10)]
    [TestCase(int.MinValue + 1)]
    public void Validate_GivenTooHighInteger_ThrowArgumentException(int maxValue)
    {
        Assert.That(() => IntegerValidator.Validate(_defaultInput, int.MinValue, maxValue), Throws.ArgumentException);
    }

    [TestCase(0, 10)]
    [TestCase(4, 6)]
    [TestCase(5, 6)]
    [TestCase(-5, 10)]
    public void Validate_GivenValidInteger_ThrowNothing(int minValue, int maxValue)
    {
        Assert.That(() => IntegerValidator.Validate(_defaultInput, minValue, maxValue), Throws.Nothing);
    }

    [Test]
    public void Validate_GivenMinValueAndMaxValueEqual_ThrowArgumentException()
    {
        Assert.That(() => IntegerValidator.Validate(1, 1, 1), Throws.ArgumentException);
    }
}
