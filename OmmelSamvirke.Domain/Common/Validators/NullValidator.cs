namespace OmmelSamvirke.Domain.Common.Validators;

/// <summary>
/// Validates if an object is null
/// </summary>
public class NullValidator
{
    /// <summary>
    /// Perform the validation action of <see cref="NullValidator"/>
    /// </summary>
    /// <param name="obj">The object to be validated.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="obj"/> is null.
    /// </exception>
    public static void Validate(object obj)
    {
        if (obj is null)
        {
            throw new ArgumentException("The parameter cannot be null");
        }
    }
}
