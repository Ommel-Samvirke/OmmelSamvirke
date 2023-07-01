using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Repositories;

public interface INewsletterRepository : IGenericRepository<Newsletter>
{
    Task<int> AddLike();
    Task<int> RemoveLike();
    Task<bool> RecordSentDate(DateTime sentDate);
}
