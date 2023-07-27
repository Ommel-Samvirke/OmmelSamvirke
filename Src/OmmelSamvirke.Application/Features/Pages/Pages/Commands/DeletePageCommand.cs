using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class DeletePageCommand : IRequest<bool>
{
    public int PageId { get; init; }
}

public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, bool>
{
    private readonly ILayoutConfigurationRepository _layoutConfigurationRepository;
    private readonly IPageRepository _pageRepository;

    public DeletePageCommandHandler(
        IPageRepository pageRepository,
        ILayoutConfigurationRepository layoutConfigurationRepository
    )
    {
        _layoutConfigurationRepository = layoutConfigurationRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<bool> Handle(DeletePageCommand request, CancellationToken cancellationToken)
    {
        DeletePageCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page page = (await _pageRepository.GetByIdWithRelationsAsync(request.PageId, cancellationToken))!;

        bool pageIsDeleted = await _pageRepository.DeleteAsync(page, cancellationToken);
        bool layoutsAreDeleted = await _layoutConfigurationRepository.DeleteByPageAsync(page, cancellationToken);
        
        return pageIsDeleted && layoutsAreDeleted;
    }
}
