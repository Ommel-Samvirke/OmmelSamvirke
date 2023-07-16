using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    private readonly IPageRepository _pageRepository;

    public UpdatePageCommandValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        
        RuleFor(p => p.OriginalPage.Id)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
        
        RuleFor(p => p.OriginalPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters");

        RuleFor(p => p.OriginalPage.Template)
            .NotNull()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page template must be set");
        
        RuleFor(p => p.UpdatedPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters");
        
        RuleFor(p => p.UpdatedPage.PageTemplateId)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
        
        RuleFor(p => p.UpdatedContentBlockDataElements)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Updated content blocks must be set");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageTemplateId) is not null;
    }
}
