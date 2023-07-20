using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    public UpdatePageCommandValidator(
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository
    )
    {
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
            .WithMessage("Updated content blocks must be set");
    }
}
