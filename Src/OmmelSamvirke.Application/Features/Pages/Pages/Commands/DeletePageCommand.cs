using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class DeletePageCommand : IRequest<bool>
{
    public int PageId { get; set; }

    public DeletePageCommand(int pageId)
    {
        PageId = pageId;
    }
}

public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, bool>
{
    private readonly IPageRepository _pageRepository;

    public DeletePageCommandHandler(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }
    
    public async Task<bool> Handle(DeletePageCommand request, CancellationToken cancellationToken)
    {
        DeletePageCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page page = (await _pageRepository.GetByIdAsync(request.PageId))!;

        return await _pageRepository.DeleteAsync(page);
    }
}
