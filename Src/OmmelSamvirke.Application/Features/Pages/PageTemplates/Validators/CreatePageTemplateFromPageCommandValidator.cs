using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateFromPageCommandValidator : AbstractValidator<CreatePageTemplateFromPageCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;

    public CreatePageTemplateFromPageCommandValidator(
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository
    )
    {
        _pageRepository = pageRepository;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;

        RuleFor(p => p.PageUpdateDto.Id)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist")
            .MustAsync(ContentBlocksMustExist)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page does not have any content blocks")
            .MustAsync(ContentBlocksMustNotOverlap)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page content blocks must not overlap");
        
        RuleFor(p => p.PageUpdateDto.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name cannot be longer than 200 characters")
            .MustAsync(NameMustBeUnique)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Template name must be unique");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        Page? page = await _pageRepository.GetByIdAsync(pageId, cancellationToken);
        return page is not null;
    }
    
    private async Task<bool> ContentBlocksMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        List<ContentBlock> contentBlocks = await _contentBlockRepository.GetByPageTemplateIdAsync(pageTemplateId, cancellationToken);
        return contentBlocks.Count > 0;
    }

    private async Task<bool> ContentBlocksMustNotOverlap(int pageTemplateId, CancellationToken cancellationToken)
    {
        List<ContentBlock> contentBlocks = await _contentBlockRepository.GetByPageTemplateIdAsync(pageTemplateId, cancellationToken);
        return ContentBlock.AreAnyBlocksOverlapping(contentBlocks);
    }
    
    private async Task<bool> NameMustBeUnique(string name, CancellationToken cancellationToken)
    {
        IReadOnlyList<PageTemplate> pageTemplates = await _pageTemplateRepository.GetAsync(cancellationToken);
        return pageTemplates.All(p => p.Name != $"{name}-template");
    }
}
