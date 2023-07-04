using FluentValidation;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateCommandValidator : AbstractValidator<CreatePageTemplateCommand>
{
    public CreatePageTemplateCommandValidator()
    {
        RuleFor(p => p.PageTemplateState)
            .IsInEnum()
            .WithMessage("{PropertyName} must be a valid value in the PageTemplateState enum");

        RuleFor(p => p.ContentBlocks)
            .NotNull()
            .WithMessage("{PropertyName} cannot be null")
            .Must(p => p.Count > 0)
            .WithMessage("{PropertyName} cannot have a size less than 1");


        RuleFor(p => p.Name)
            .NotNull()
            .WithMessage("{PropertyName} cannot be null")
            .Must(p => p.Length > 2)
            .WithMessage("{PropertyName} cannot have a size less than 3")
            .Must(p => p.Length <= 50)
            .WithMessage("{PropertyName} cannot have a size greater than 50");
    }
}
