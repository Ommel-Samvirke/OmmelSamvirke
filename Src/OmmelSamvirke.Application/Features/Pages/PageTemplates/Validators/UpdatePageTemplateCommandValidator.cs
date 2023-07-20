using AutoMapper;
using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class UpdatePageTemplateCommandValidator : AbstractValidator<UpdatePageTemplateCommand>
{
    private readonly IMapper _mapper;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IPageRepository _pageRepository;

    public UpdatePageTemplateCommandValidator(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IPageRepository pageRepository
    )
    {
        _mapper = mapper;
        _contentBlockRepository = contentBlockRepository;
        _pageRepository = pageRepository;

        RuleFor(p => p.OriginalPageTemplate.Id)
            .MustAsync(pageTemplateRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist")
            .MustAsync((p, _, cancellationToken) => PageTemplateMustNotUpdateContentBlocksIfInUse(p, cancellationToken))
            .WithErrorCode(ErrorCode.ResourceInUse)
            .WithMessage("The ContentBlocks of the PageTemplate cannot be updated, because the PageTemplate is in use");
        
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
            .WithMessage("Name cannot be longer than 225 characters")
            .MustAsync((p, cancellationToken) => pageTemplateRepository.IsPropertyUniqueAsync("Name", p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Template Name must be unique");
        
        RuleFor(p => p.UpdatedPageTemplate.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");
        
        RuleFor(p => p.UpdatedPageTemplate.ContentBlocks.Count)
            .GreaterThan(0)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Template must have at least one content block");
    }

    private async Task<bool> ContentBlocksMustNotOverlap(int pageTemplateId, CancellationToken cancellationToken)
    {
        List<ContentBlock> contentBlocks = await _contentBlockRepository.GetByPageTemplateIdAsync(pageTemplateId, cancellationToken);
        return !ContentBlock.AreAnyBlocksOverlapping(contentBlocks);
    }

    private async Task<bool> PageTemplateMustNotUpdateContentBlocksIfInUse(UpdatePageTemplateCommand command, CancellationToken cancellationToken)
    {
        List<ContentBlockCreateDto> originalContentBlocks =
            _mapper.Map<List<ContentBlockCreateDto>>(command.OriginalPageTemplate.ContentBlocks);
        List<ContentBlockCreateDto> updatedContentBlocks = command.UpdatedPageTemplate.ContentBlocks;

        bool isOriginalSubsetOfUpdated = !originalContentBlocks.Select(item => item.Id)
            .Except(updatedContentBlocks.Select(item => item.Id)).Any();

        bool isUpdatedSubsetOfOriginal = !updatedContentBlocks.Select(item => item.Id)
            .Except(originalContentBlocks.Select(item => item.Id)).Any();

        bool contentBlocksAreEqual = isOriginalSubsetOfUpdated && isUpdatedSubsetOfOriginal;

        if (contentBlocksAreEqual)
            return true;

        IReadOnlyList<Page> pages = await _pageRepository.GetAsync(cancellationToken);
        return pages.All(p => p.TemplateId != command.OriginalPageTemplate.Id);
    }
}
