using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateCommandValidator : AbstractValidator<CreatePageTemplateCommand>
{
    public CreatePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        RuleFor(p => p.PageTemplateCreateDto.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(225)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 200 characters")
            .MustAsync((p, cancellationToken) => pageTemplateRepository.IsPropertyUniqueAsync("Name", p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Template Name must be unique");
        
        RuleFor(p => p.PageTemplateCreateDto.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");
    }
}
