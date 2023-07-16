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
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(p => p.PageTemplateId)
           .MustAsync(PageTemplateMustExist)
           .WithErrorCode(ErrorCode.ResourceNotFound)
           .WithMessage("Page template with id {PropertyValue} was not found.");
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplateId) is not null;
    }
}
