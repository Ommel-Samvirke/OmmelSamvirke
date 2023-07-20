using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class GetPageTemplateQueryValidator : AbstractValidator<GetPageTemplateQuery>
{
    public GetPageTemplateQueryValidator(IPageTemplateRepository pageTemplateRepository)
    {
        RuleFor(p => p.PageTemplateId)
           .MustAsync(pageTemplateRepository.ExistsAsync)
           .WithErrorCode(ErrorCode.ResourceNotFound)
           .WithMessage("Page template with id {PropertyValue} was not found.");
    }
}
