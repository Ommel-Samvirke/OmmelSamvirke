using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Admins.Models;

public class Admin : BaseModel
{
    /// <summary>
    /// Create a new instance of an <see cref="Admin"/>.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    public Admin()
    {
        
    }
    
    /// <summary>
    /// Create an instance of an <see cref="Admin"/> that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    public Admin(int id, DateTime dateCreated, DateTime dateModified) : base(id, dateCreated, dateModified)
    { }
}
