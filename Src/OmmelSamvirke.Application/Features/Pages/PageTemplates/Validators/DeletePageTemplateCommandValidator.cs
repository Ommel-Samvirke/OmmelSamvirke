using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class DeletePageTemplateCommandValidator : AbstractValidator<DeletePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;

    public DeletePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository, IPageRepository pageRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        _pageRepository = pageRepository;
        
        RuleFor(p => p.PageTemplateId)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("The PageTemplate does not exist.")
            .MustAsync(PageTemplateMustNotBeImplementedByAnyPages)
            .WithErrorCode(ErrorCode.ResourceInUse)
            .WithMessage("The PageTemplate cannot be deleted because it is used by one or more Pages.");
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        PageTemplate? pageTemplate = await _pageTemplateRepository.GetByIdAsync(pageTemplateId);
        return pageTemplate is not null;
    }
    
    private async Task<bool> PageTemplateMustNotBeImplementedByAnyPages(int pageTemplateId, CancellationToken cancellationToken)
    {
        List<Page> pagesImplementingTemplate = await _pageRepository.GetByPageTemplateId(pageTemplateId);
        return pagesImplementingTemplate.Count == 0;
    }
}
