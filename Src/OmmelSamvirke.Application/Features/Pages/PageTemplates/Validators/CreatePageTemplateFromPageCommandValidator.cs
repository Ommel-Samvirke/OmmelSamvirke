using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateFromPageCommandValidator : AbstractValidator<CreatePageTemplateFromPageCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepository _contentBlockRepository;

    public CreatePageTemplateFromPageCommandValidator(
        IPageRepository pageRepository,
        IContentBlockDataRepository contentBlockRepository
    )
    {
        _pageRepository = pageRepository;
        _contentBlockRepository = contentBlockRepository;

        RuleFor(x => x.Page.Id)
            .NotNull()
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page id must not be null");

        RuleFor(x => x.Page)
            .NotNull()
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist")
            .MustAsync(PageExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist")
            .MustAsync(PageMustHaveContentBlocks)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("The page does not have any Content Blocks");

        RuleFor(x => x.Page.Name)
            .NotNull()
            .WithMessage("Page name must not be null");
    }
    
    private async Task<bool> PageExists(PageDto page, CancellationToken cancellationToken)
    {
        Page? foundPage = await _pageRepository.GetByIdAsync(page.Id);
        return foundPage != null;
    }
    
    private async Task<bool> PageMustHaveContentBlocks(PageDto page, CancellationToken cancellationToken)
    {
        List<IContentBlockData> contentBlocks = await _contentBlockRepository.GetByPageIdAsync(page.Id);
        return contentBlocks.Count > 0;
    }
}
