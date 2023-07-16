namespace OmmelSamvirke.Domain.Common;

/// <summary>
/// BaseModel contains the basic fields that all models must contain.
/// Thus, all models must extend from this class.
/// </summary>
public abstract class BaseModel : IEquatable<BaseModel>
{
    /// <summary>
    /// The integer Id of a model instance.
    /// It is assumed that any DB used with Ommel Samvirke will
    /// use integer identifiers for all table types.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Describes when the instance of the model was first created.
    /// </summary>
    public DateTime? DateCreated { get; set; }

    /// <summary>
    /// Describes when the instance of the model was last modified.
    /// </summary>
    public DateTime? DateModified { get; set; }

    /// <summary>
    /// Checks the equality of two models based
    /// on the value of the fields <see cref="Id"/>, <see cref="DateCreated"/>,
    /// and <see cref="DateModified"/>.
    /// </summary>
    /// <param name="other">The other model to check against.</param>
    /// <returns>True if equal, otherwise false.</returns>
    public bool Equals(BaseModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && DateCreated.Equals(other.DateCreated) && DateModified.Equals(other.DateModified);
    }

    /// <summary>
    /// Checks the equality of this model instance and any object.
    /// </summary>
    /// <param name="obj">Any object type</param>
    /// <returns>True if equal, otherwise false</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((BaseModel)obj);
    }

    /// <summary>
    /// Get the hash code of this model instance.
    /// The hash code is generated based on
    /// the properties <see cref="Id"/> and <see cref="DateCreated"/>.
    /// </summary>
    /// <returns>The integer Hash Code of this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, DateCreated);
    }
}
