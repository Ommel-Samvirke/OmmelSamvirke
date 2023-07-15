using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class GetPageTemplateQueryValidator : AbstractValidator<GetPageTemplateQuery>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public GetPageTemplateQueryValidator(IPageTemplateRepository pageTemplateRepository)
    {
       
    }

}
