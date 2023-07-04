using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class UpdatePageTemplateContentBlockCommandValidator : AbstractValidator<UpdatePageTemplateContentBlockCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;

    public UpdatePageTemplateContentBlockCommandValidator(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        
        RuleFor(x => x.PageTemplate)
            .NotNull()
            .WithMessage("Page template cannot be null.")
            .Must(pageTemplate => pageTemplate.Id != null)
            .WithMessage("Page template must have an id.")
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist.");

        RuleFor(x => x.ContentBlock)
            .NotNull()
            .WithMessage("Content block cannot be null.")
            .Must(contentBlock => contentBlock.Id != null)
            .WithMessage("Content block must have an id.")
            .MustAsync(ContentBlockMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Content block does not exist.")
            .Must((command, contentBlock) => command.PageTemplate.Blocks.Any(cb => cb.Id == contentBlock.Id))
            .WithMessage("Content block must be contained in the page template.");
    }
    
    private async Task<bool> PageTemplateMustExist(PageTemplate pageTemplate, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync((int)pageTemplate.Id!) is not null;
    }
    
    private async Task<bool> ContentBlockMustExist(ContentBlock contentBlock, CancellationToken cancellationToken)
    {
        return await _contentBlockRepository.GetByIdAsync((int)contentBlock.Id!) is not null;
    }
}
