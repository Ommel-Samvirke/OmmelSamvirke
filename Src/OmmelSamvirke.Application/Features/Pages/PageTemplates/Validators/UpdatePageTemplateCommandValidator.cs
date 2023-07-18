using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class UpdatePageTemplateCommandValidator : AbstractValidator<UpdatePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockDataRepositoriesAggregate _contentBlockDataRepositoriesAggregate;

    public UpdatePageTemplateCommandValidator(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockDataRepositoriesAggregate = contentBlockDataRepositoriesAggregate;

        RuleFor(p => p.OriginalPageTemplate.Id)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
        
        RuleFor(p => p.OriginalPageTemplate.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(225)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 225 characters");
        
        RuleFor(p => p.OriginalPageTemplate.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");
        
        RuleFor(p => p.UpdatedPageTemplate.ContentBlocks.Count)
            .GreaterThan(0)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page template must have at least one content block");
        
        RuleFor(p => p.UpdatedPageTemplate.Id)
            .MustAsync(ContentBlocksMustNotOverlap)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Content blocks must not overlap");
        
        RuleFor(p => p.UpdatedPageTemplate.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(225)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 225 characters");
        
        RuleFor(p => p.UpdatedPageTemplate.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");
        
        RuleFor(p => p.UpdatedPageTemplate.ContentBlocks.Count)
            .GreaterThan(0)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page template must have at least one content block");
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplateId, cancellationToken) is not null;
    }
    
    private async Task<bool> ContentBlocksMustNotOverlap(int pageId, CancellationToken cancellationToken)
    {
        List<IContentBlockData> contentBlockData = await _contentBlockDataRepositoriesAggregate.GetByPageIdAsync(pageId, cancellationToken);
        List<ContentBlock> contentBlocks = contentBlockData
            .Select(c => c.BaseContentBlock)
            .Where(c => c is not null)
            .ToList()!;

        return ContentBlock.AreAnyBlocksOverlapping(contentBlocks);
    }
}
