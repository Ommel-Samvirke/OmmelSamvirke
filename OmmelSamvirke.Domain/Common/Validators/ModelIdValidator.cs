namespace OmmelSamvirke.Domain.Common.Validators;

/// <summary>
/// Validates if the id of a model is valid.
/// </summary>
public static class ModelIdValidator
{
    /// <summary>
    /// Performs the validation action of <see cref="ModelIdValidator"/>.
    /// </summary>
    /// <param name="id">The integer id of the model.</param>
    /// <exception cref="ArgumentException">Thrown if the id is less than or equal to zero.</exception>
    public static void Validate(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("A model Id cannot be less than or equal to zero");
        } 
    }
}
