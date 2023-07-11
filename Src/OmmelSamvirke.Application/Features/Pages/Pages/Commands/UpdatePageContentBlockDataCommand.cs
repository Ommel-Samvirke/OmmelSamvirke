using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageContentBlockDataCommand : IRequest<List<IContentBlockData>>
{
    public List<IContentBlockData> OriginalState { get; }
    public List<IContentBlockData> UpdatedState { get; }

    public UpdatePageContentBlockDataCommand(List<IContentBlockData> originalState, List<IContentBlockData> updatedState)
    {
        OriginalState = originalState;
        UpdatedState = updatedState;
    }   
}

public class UpdatePageContentBlockDataCommandHandler : IRequestHandler<UpdatePageContentBlockDataCommand, List<IContentBlockData>>
{
    private readonly IContentBlockDataRepository _contentBlockDataRepository;

    public UpdatePageContentBlockDataCommandHandler(IContentBlockDataRepository contentBlockDataRepository)
    {
        _contentBlockDataRepository = contentBlockDataRepository;
    }
    
    public async Task<List<IContentBlockData>> Handle(UpdatePageContentBlockDataCommand request, CancellationToken cancellationToken)
    {
        UpdatePageContentBlockDataCommandValidator validator = new(_contentBlockDataRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        return await _contentBlockDataRepository.UpdateAsync(request.UpdatedState);
    }
}
