using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateFromPageCommandValidator : AbstractValidator<CreatePageTemplateFromPageCommand>
{
    private readonly IContentBlockRepository _contentBlockRepository;

    public CreatePageTemplateFromPageCommandValidator(
        IPageRepository pageRepository,
        IContentBlockRepository contentBlockRepository
    )
    {
        _contentBlockRepository = contentBlockRepository;

        RuleFor(p => p.PageUpdateDto.Id)
            .MustAsync(pageRepository.ExistsAsync)
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
            .MustAsync((p, cancellationToken) => pageRepository.IsPropertyUniqueAsync("Name", p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Template name must be unique");
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
}
