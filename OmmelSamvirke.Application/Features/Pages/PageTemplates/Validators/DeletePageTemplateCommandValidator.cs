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

        RuleFor(x => x.PageTemplateId)
            .MustAsync(PageTemplateExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("PageTemplate does not exist");
        
        RuleFor(x => x.PageTemplateId)
            .MustAsync(PageTemplateIsNotInUse)
            .WithErrorCode(ErrorCode.ResourceInUse)
            .WithMessage("PageTemplate is in use");
    }
    
    private async Task<bool> PageTemplateExists(int pageTemplateId, CancellationToken cancellationToken)
    {
        PageTemplate? pageTemplate = await _pageTemplateRepository.GetByIdAsync(pageTemplateId);
        return pageTemplate != null;
    }
    
    private async Task<bool> PageTemplateIsNotInUse(int pageTemplateId, CancellationToken cancellationToken)
    {
        IReadOnlyList<Page> pages = await _pageRepository.GetAsync();
        return pages.All(page => page.Template.Id != pageTemplateId);
    }
}
