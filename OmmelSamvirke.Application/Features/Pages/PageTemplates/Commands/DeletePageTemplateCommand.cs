using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class DeletePageTemplateCommand : IRequest<bool>
{
    public int PageTemplateId { get; }

    public DeletePageTemplateCommand(int pageTemplateId)
    {
        PageTemplateId = pageTemplateId;
    }
}

public class DeletePageTemplateCommandHandler : IRequestHandler<DeletePageTemplateCommand, bool>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;

    public DeletePageTemplateCommandHandler(
        IPageTemplateRepository pageTemplateRepository,
        IPageRepository pageRepository
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _pageRepository = pageRepository;
    }
    
    public async Task<bool> Handle(DeletePageTemplateCommand request, CancellationToken cancellationToken)
    {
        DeletePageTemplateCommandValidator validator = new(_pageTemplateRepository, _pageRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        ValidationResultHandler.Handle(validationResult, request);

        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId))!;

        return await _pageTemplateRepository.DeleteAsync(pageTemplate);
    }
}
