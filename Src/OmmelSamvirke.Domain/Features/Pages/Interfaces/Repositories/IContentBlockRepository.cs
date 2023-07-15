using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockRepository : IGenericRepository<ContentBlock>
{
    Task<List<ContentBlock>> CreateAsync(List<ContentBlock> contentBlocks);
    Task<bool> DeleteAsync(List<ContentBlock> contentBlocks);
}
