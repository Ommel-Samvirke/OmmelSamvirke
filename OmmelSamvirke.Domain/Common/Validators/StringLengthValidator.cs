namespace OmmelSamvirke.Domain.Common.Validators;

/// <summary>
/// Validate the length of a string.
/// </summary>
public static class StringLengthValidator
{
    /// <summary>
    /// Perform string length validation.
    /// </summary>
    /// <param name="testString">The string to be validated.</param>
    /// <param name="minLength">The minimum acceptable length of the string.</param>
    /// <param name="maxLength">The maximum acceptable length of the string.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="testString"/> is shorter than <paramref name="minLength"/>
    /// or longer than <paramref name="maxLength"/>.
    /// </exception>
    public static void Validate(string testString, int minLength, int maxLength = int.MaxValue)
    {
        if (minLength == maxLength)
        {
            throw new ArgumentException("The parameters minValue and maxValue cannot be equal");
        }
        
        if (string.IsNullOrEmpty(testString))
        {
            throw new ArgumentException($"The property must not be null or empty");
        }
        
        if (testString.Length < minLength)
        {
            throw new ArgumentException($"The property must be at least {minLength} characters long");
        }
        
        if (testString.Length > maxLength)
        {
            throw new ArgumentException($"The property must be no longer than {maxLength} characters long");
        }
    }
}