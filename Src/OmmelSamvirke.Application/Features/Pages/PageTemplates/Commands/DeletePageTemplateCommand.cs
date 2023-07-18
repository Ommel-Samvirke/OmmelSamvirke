using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class DeletePageTemplateCommand : IRequest<bool>
{
    public int PageTemplateId { get; init; }
}

public class DeletePageTemplateCommandHandler : IRequestHandler<DeletePageTemplateCommand, bool>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IPageRepository _pageRepository;

    public DeletePageTemplateCommandHandler(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IPageRepository pageRepository
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<bool> Handle(DeletePageTemplateCommand request, CancellationToken cancellationToken)
    {
        DeletePageTemplateCommandValidator validator = new(_pageTemplateRepository, _pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId, cancellationToken))!;
        List<ContentBlock> contentBlocks = await _contentBlockRepository.GetByPageTemplateIdAsync(pageTemplate.Id, cancellationToken);
        
        await _contentBlockRepository.DeleteAsync(contentBlocks, cancellationToken);

        return await _pageTemplateRepository.DeleteAsync(pageTemplate, cancellationToken);
    }
}
