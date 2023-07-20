using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    private readonly IContentBlockRepository _contentBlockRepository;

    public UpdatePageCommandValidator(
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository
    )
    {
        _contentBlockRepository = contentBlockRepository;
        
        RuleFor(p => p.OriginalPage.Id)
            .MustAsync(pageRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
        
        RuleFor(p => p.OriginalPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters");

        RuleFor(p => p.UpdatedPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters")
            .MustAsync((p, cancellationToken) => pageRepository.IsPropertyUniqueAsync("Name",p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Name must be unique");
        
        RuleFor(p => p.UpdatedPage.PageTemplateId)
            .MustAsync(pageTemplateRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");

        RuleFor(p => p.UpdatedContentBlockDataElements)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Updated content blocks must be set")
            .MustAsync(NonOptionalContentBlocksMustHaveData)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Non-optional content blocks must have data");
    }
    
    private async Task<bool> NonOptionalContentBlocksMustHaveData(
        List<IContentBlockDataDto> contentBlockDataDtos,
        CancellationToken cancellationToken
    )
    {
        foreach (IContentBlockDataDto contentBlockDataDto in contentBlockDataDtos)
        {
            ContentBlock? contentBlock = await _contentBlockRepository.GetByIdAsync(contentBlockDataDto.BaseContentBlockId, cancellationToken);

            if (contentBlock is null)
                return false;
            
            if (contentBlock.IsOptional)
                continue;

            if (!ValidateNonOptionalContentBlocks(contentBlockDataDto)) return false;
        }

        return true;
    }

    private static bool ValidateNonOptionalContentBlocks(IContentBlockDataDto contentBlockDataDto)
    {
        return contentBlockDataDto.ContentBlockType switch
        {
            ContentBlockType.HeadlineBlock => !string.IsNullOrWhiteSpace(((HeadlineBlockDataDto)contentBlockDataDto).Headline),
            ContentBlockType.ImageBlock => !string.IsNullOrWhiteSpace(((ImageBlockDataDto)contentBlockDataDto).ImageUrl),
            ContentBlockType.PdfBlock => !string.IsNullOrWhiteSpace(((PdfBlockDataDto)contentBlockDataDto).PdfUrl),
            ContentBlockType.SlideshowBlock => ((SlideshowBlockDataDto)contentBlockDataDto).ImageUrls.Count > 0,
            ContentBlockType.TextBlock => !string.IsNullOrWhiteSpace(((TextBlockDataDto)contentBlockDataDto).Text),
            ContentBlockType.VideoBlock => !string.IsNullOrWhiteSpace(((VideoBlockDataDto)contentBlockDataDto).VideoUrl),
            _ => false
        };
    }
}
