using OmmelSamvirke.Domain.Common.Validators;

namespace OmmelSamvirke.Domain.UnitTests.Common.Validators;

public class ModelIdValidatorTests
{
    [Test]
    public void Validate_GivenPositiveId_ReturnTrue()
    {
        Assert.That(() => ModelIdValidator.Validate(1), Throws.Nothing);
    }

    [Test]
    public void Validate_GivenZeroId_ReturnFalse()
    {
        Assert.That(() => ModelIdValidator.Validate(0), Throws.ArgumentException);
    }

    [Test]
    public void Validate_GivenNegativeId_ReturnFalse()
    {
        Assert.That(() => ModelIdValidator.Validate(-1), Throws.ArgumentException);
    }
}
