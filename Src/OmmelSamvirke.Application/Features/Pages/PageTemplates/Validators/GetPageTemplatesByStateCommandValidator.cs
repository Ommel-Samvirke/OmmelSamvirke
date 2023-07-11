using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class GetPageTemplatesByStateCommandValidator : AbstractValidator<GetPageTemplatesByStateCommand>
{
    public GetPageTemplatesByStateCommandValidator()
    {
        RuleFor(x => x.PageTemplateState)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("The page template state is not valid.");
    }
}
