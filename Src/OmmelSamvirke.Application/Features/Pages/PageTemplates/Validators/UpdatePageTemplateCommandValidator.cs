using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class UpdatePageTemplateCommandValidator : AbstractValidator<UpdatePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public UpdatePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(x => x.OriginalPageTemplate)
            .NotNull()
            .WithMessage("Original page template must be provided")
            .MustAsync(OriginalPageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Original page template does not exist");
        
        RuleFor(x => x.UpdatedPageTemplate)
            .NotNull()
            .WithMessage("Updated page template must be provided");
    }
    
    private async Task<bool> OriginalPageTemplateMustExist(PageTemplateDto originalPageTemplate, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(originalPageTemplate.Id) is not null;
    }
}
