using FluentValidation;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class SavePageTemplateVersionCommandValidator : AbstractValidator<SavePageTemplateVersionCommand>
{
    public SavePageTemplateVersionCommandValidator()
    {
        RuleFor(x => x.PageTemplateVersion.Id)
            .NotNull()
            .WithMessage("PageTemplateVersion id must not be null");
        
        RuleFor(x => x.PageTemplateVersion.Name)
            .NotNull()
            .WithMessage("PageTemplateVersion name must not be null");
        
        RuleFor(x => x.PageTemplateVersion.ContentBlocks)
            .NotNull()
            .WithMessage("PageTemplateVersion content blocks must not be null");
    }
}
