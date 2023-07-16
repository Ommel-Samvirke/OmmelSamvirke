using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class GetPageTemplatesByStateQueryValidator : AbstractValidator<GetPageTemplatesByStateQuery>
{
    public GetPageTemplatesByStateQueryValidator()
    {
        RuleFor(p => p.PageTemplateState)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithErrorCode("PageTemplateState must be a valid PageTemplateState");
    }
}
