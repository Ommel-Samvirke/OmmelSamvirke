using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class AddContentBlockToPageTemplateCommandValidator : AbstractValidator<AddContentBlockToPageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public AddContentBlockToPageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        RuleFor(x => x.ContentBlock)
            .NotNull()
            .WithMessage("Content block cannot be null.");

        RuleFor(x => x.PageTemplate)
            .NotNull()
            .WithMessage("Page template cannot be null.")
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist.");
    }
    
    private async Task<bool> PageTemplateMustExist(PageTemplate pageTemplate, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync((int)pageTemplate.Id!) is not null;
    }
}
