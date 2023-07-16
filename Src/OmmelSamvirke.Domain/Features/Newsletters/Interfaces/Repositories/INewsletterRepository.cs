using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Repositories;

public interface INewsletterRepository : IGenericRepository<Newsletter>
{
    Task<int> AddLike(CancellationToken cancellationToken = default);
    Task<int> RemoveLike(CancellationToken cancellationToken = default);
    Task<bool> RecordSentDate(DateTime sentDate, CancellationToken cancellationToken = default);
}
