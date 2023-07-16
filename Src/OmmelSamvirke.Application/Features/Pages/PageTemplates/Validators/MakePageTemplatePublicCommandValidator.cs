using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class MakePageTemplatePublicCommandValidator : AbstractValidator<MakePageTemplatePublicCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public MakePageTemplatePublicCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(p => p.PageTemplateId)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplateId) is not null;
    }
}
