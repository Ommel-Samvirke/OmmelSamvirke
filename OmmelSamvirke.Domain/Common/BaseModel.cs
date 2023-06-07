namespace OmmelSamvirke.Domain.Common;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
