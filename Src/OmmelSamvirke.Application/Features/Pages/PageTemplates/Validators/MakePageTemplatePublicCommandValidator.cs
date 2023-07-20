using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class MakePageTemplatePublicCommandValidator : AbstractValidator<MakePageTemplatePublicCommand>
{
    public MakePageTemplatePublicCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        RuleFor(p => p.PageTemplateId)
            .MustAsync(pageTemplateRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
    }
}
