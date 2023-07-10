using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class MakePageTemplatePublicCommandValidator : AbstractValidator<MakePageTemplatePublicCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public MakePageTemplatePublicCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(x => x.PageTemplateId)
            .MustAsync(PageTemplateExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("PageTemplate does not exist");

        RuleFor(p => p.CurrentTemplateState)
            .Must(BeInValidState)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("PageTemplate must be in the archived or custom state to become public");
    }

    private async Task<bool> PageTemplateExists(int pageTemplateId, CancellationToken cancellationToken)
    {
        PageTemplate? pageTemplate = await _pageTemplateRepository.GetByIdAsync(pageTemplateId);
        return pageTemplate != null;
    }
    
    private static bool BeInValidState(PageTemplateState pageTemplateState)
    {
        return pageTemplateState is PageTemplateState.Archived or PageTemplateState.Custom;
    }
}
