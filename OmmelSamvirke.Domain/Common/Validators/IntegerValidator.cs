namespace OmmelSamvirke.Domain.Common.Validators;

/// <summary>
/// Validates if the value of an integer is valid
/// </summary>
public static class IntegerValidator
{
    /// <summary>
    /// Performs the validation action of <see cref="IntegerValidator"/>.
    /// </summary>
    /// <param name="testInt">The integer to be validated.</param>
    /// <param name="minValue">The minimum acceptable value of <paramref name="testInt"/>.</param>
    /// <param name="maxValue">The maximum acceptable value of <paramref name="testInt"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the integer value is less <paramref name="minValue"/>
    /// or larger than <paramref name="maxValue"/>.
    /// </exception>
    public static void Validate(int testInt, int minValue, int maxValue = int.MaxValue)
    {
        if (minValue == maxValue)
        {
            throw new ArgumentException("The parameters minValue and maxValue cannot be equal");
        }
        
        if (testInt < minValue)
        {
            throw new ArgumentException($"The property cannot have a value lower than {minValue}");
        }

        if (testInt > maxValue)
        {
            throw new ArgumentException($"The property cannot have a value greater than {maxValue}");
        }
    }
}
