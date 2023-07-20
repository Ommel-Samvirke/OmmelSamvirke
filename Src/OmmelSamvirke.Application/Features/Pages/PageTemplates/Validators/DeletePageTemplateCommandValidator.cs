using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class DeletePageTemplateCommandValidator : AbstractValidator<DeletePageTemplateCommand>
{
    private readonly IPageRepository _pageRepository;

    public DeletePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository, IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        
        RuleFor(p => p.PageTemplateId)
            .MustAsync(pageTemplateRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("The PageTemplate does not exist.")
            .MustAsync(PageTemplateMustNotBeImplementedByAnyPages)
            .WithErrorCode(ErrorCode.ResourceInUse)
            .WithMessage("The PageTemplate cannot be deleted because it is used by one or more Pages.");
    }

    private async Task<bool> PageTemplateMustNotBeImplementedByAnyPages(int pageTemplateId, CancellationToken cancellationToken)
    {
        List<Page> pagesImplementingTemplate = await _pageRepository.GetByPageTemplateId(pageTemplateId, cancellationToken);
        return pagesImplementingTemplate.Count == 0;
    }
}
