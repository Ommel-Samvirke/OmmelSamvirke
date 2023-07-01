using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterRepository : IGenericRepository<Newsletter>
{
    Task<bool> AddLike();
    Task<bool> RemoveLike();
    Task<bool> RecordSentDate(DateTime sentDate);
}
