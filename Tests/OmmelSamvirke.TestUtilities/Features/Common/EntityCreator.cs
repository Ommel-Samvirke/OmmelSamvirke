using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.TestUtilities.Features.Common;

public static class EntityCreator
{
    public static T Create<T>(T entity) where T : BaseModel
    {
        entity.DateCreated = DateTime.UtcNow;
        entity.DateModified = entity.DateCreated;
        return entity;
    }
}
