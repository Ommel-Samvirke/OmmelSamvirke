namespace OmmelSamvirke.Domain.Common;

/// <summary>
/// BaseModel contains the basic fields that all models must contain.
/// Thus, all models must extend from this class.
/// </summary>
public abstract class BaseModel
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
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    /// Describes when the instance of the model was last modified.
    /// </summary>
    public DateTime DateModified { get; set; }
}
