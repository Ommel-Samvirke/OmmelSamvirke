using FluentValidation;
using OmmelSamvirke.Application.Errors;
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
        
        RuleFor(p => p.PageUpdateDto.Id)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist")
            .MustAsync(ContentBlocksMustExist)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page does not have any content blocks");
        
        RuleFor(p => p.PageUpdateDto.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(190)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 190 characters so '-template' can be added to Page Template name");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        Page? page = await _pageRepository.GetByIdAsync(pageId);
        return page is not null;
    }
    
    private async Task<bool> ContentBlocksMustExist(int pageId, CancellationToken cancellationToken)
    {
        List<IContentBlockData> contentBlockData = await _contentBlockRepository.GetByPageIdAsync(pageId);
        return contentBlockData.Where(c => c.BaseContentBlock is not null).ToList().Count > 0;
    }
}
