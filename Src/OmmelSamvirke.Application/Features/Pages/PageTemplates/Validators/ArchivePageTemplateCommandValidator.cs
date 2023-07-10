using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class ArchivePageTemplateCommandValidator : AbstractValidator<ArchivePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public ArchivePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(x => x.PageTemplateId)
            .MustAsync(PageTemplateExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("PageTemplate does not exist");

        RuleFor(p => p.CurrentTemplateState)
            .Equal(PageTemplateState.Public)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("PageTemplate must be in the public state to be archived");
    }
    
    private async Task<bool> PageTemplateExists(int pageTemplateId, CancellationToken cancellationToken)
    {
        PageTemplate? pageTemplate = await _pageTemplateRepository.GetByIdAsync(pageTemplateId);
        return pageTemplate != null;
    }
}
