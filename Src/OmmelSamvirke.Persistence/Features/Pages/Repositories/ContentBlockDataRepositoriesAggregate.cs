using System.Reflection;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockDataRepositoriesAggregate : IContentBlockDataRepositoriesAggregate
{
    private readonly Dictionary<Type, object> _repositories;
    private readonly List<Type> _contentBlockTypes = new()
    {
        typeof(HeadlineBlock),
        typeof(ImageBlock),
        typeof(PdfBlock),
        typeof(SlideshowBlock),
        typeof(TextBlock),
        typeof(VideoBlock),
    };
    
    public ContentBlockDataRepositoriesAggregate(
        IContentBlockDataRepository<HeadlineBlockData, HeadlineBlock> headlineBlockDataRepository, 
        IContentBlockDataRepository<ImageBlockData, ImageBlock> imageBlockDataRepository,
        IContentBlockDataRepository<PdfBlockData, PdfBlock> pdfBlockDataRepository,
        IContentBlockDataRepository<SlideshowBlockData, SlideshowBlock> slideshowBlockDataRepository,
        IContentBlockDataRepository<TextBlockData, TextBlock> textBlockDataRepository,
        IContentBlockDataRepository<VideoBlockData, VideoBlock> videoBlockDataRepository)
    {
        _repositories = new Dictionary<Type, object>
        {
            {typeof(HeadlineBlockData), headlineBlockDataRepository},
            {typeof(ImageBlockData), imageBlockDataRepository},
            {typeof(PdfBlockData), pdfBlockDataRepository},
            {typeof(SlideshowBlockData), slideshowBlockDataRepository},
            {typeof(TextBlockData), textBlockDataRepository},
            {typeof(VideoBlockData), videoBlockDataRepository}
        };
    }

    public async Task<List<IContentBlockData>> GetByPageIdAsync(int pageId, CancellationToken cancellationToken = default)
    {
        List<IContentBlockData> resultList = new();
    
        foreach (Type contentBlockType in _contentBlockTypes)
        {
            object repository = _repositories[contentBlockType];
            MethodInfo? method = repository.GetType().GetMethod("GetByPageIdAsync");
            if (method == null) continue;
        
            MethodInfo generic = method.MakeGenericMethod(contentBlockType, contentBlockType.BaseType!.GenericTypeArguments[0]);
            object? result = generic.Invoke(repository, new object[] {pageId, cancellationToken});
            if (result is not Task task) continue;
            
            await task.ConfigureAwait(false);
            PropertyInfo? resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty == null) continue;
            
            List<IContentBlockData> taskResult = (List<IContentBlockData>)resultProperty.GetValue(task)!;
            resultList.AddRange(taskResult);
        }

        return resultList;
    }

    public async Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default)
    {
        return await PerformRepositoryAction(contentBlockData, "CreateAsync", cancellationToken);
    }

    public async Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default)
    {
        return await PerformRepositoryAction(contentBlockData, "UpdateAsync", cancellationToken);
    }

    public async Task<bool> DeleteAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default)
    {
        List<bool> deletionResults = new();

        foreach (IContentBlockData blockData in contentBlockData)
        {
            object repository = _repositories[blockData.GetType()];
            MethodInfo? method = repository.GetType().GetMethod("DeleteAsync");
            if (method == null) continue;

            Task task = (Task)method.Invoke(repository, new object[] { blockData, cancellationToken })!;
            await task.ConfigureAwait(false);

            PropertyInfo? resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty == null) continue;
            
            bool taskResult = (bool)(resultProperty.GetValue(task) ?? false);
            deletionResults.Add(taskResult);
        }
        
        return deletionResults.All(x => x);
    }
    
    private async Task<List<IContentBlockData>> PerformRepositoryAction(
        List<IContentBlockData> contentBlockData,
        string methodName,
        CancellationToken cancellationToken
    )
    {
        List<IContentBlockData> updatedDataList = new();
        Dictionary<Type, List<IContentBlockData>> entitiesByType = contentBlockData.GroupBy(e =>
            e.GetType()).ToDictionary(g =>
                g.Key, g => g.ToList()
        );

        foreach (KeyValuePair<Type, List<IContentBlockData>> kvp in entitiesByType)
        {
            object repository = _repositories[kvp.Key];

            MethodInfo? method = repository.GetType().GetMethod(methodName);
            if (method == null) continue;
            
            Task task = (Task)method.Invoke(repository, new object[] { kvp.Value, cancellationToken })!;
            await task.ConfigureAwait(false);

            PropertyInfo? resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty == null) continue;
            
            List<IContentBlockData> taskResult = (List<IContentBlockData>)resultProperty.GetValue(task)!;
            updatedDataList.AddRange(taskResult);
        }

        return updatedDataList;
    }
}