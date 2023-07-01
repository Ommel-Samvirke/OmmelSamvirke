using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface ISlideshowBlockDataRepository : IGenericRepository<SlideshowBlockData>
{
    Task<bool> AddImage(string imageUrl);
    Task<bool> RemoveImage(string imageUrl);
}
