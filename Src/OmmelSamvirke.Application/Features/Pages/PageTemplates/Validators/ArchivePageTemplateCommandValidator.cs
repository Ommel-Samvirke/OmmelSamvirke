using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class ArchivePageTemplateCommandValidator : AbstractValidator<ArchivePageTemplateCommand>
{
    public ArchivePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        RuleFor(p => p.PageTemplateId)
            .MustAsync(pageTemplateRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template with id {PropertyValue} does not exist.");
    }
}
