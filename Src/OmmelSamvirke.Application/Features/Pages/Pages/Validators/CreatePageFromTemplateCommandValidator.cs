using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class CreatePageFromTemplateCommandValidator : AbstractValidator<CreatePageFromTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public CreatePageFromTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(p => p.PageTemplateId)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
        
        RuleFor(p => p.PageName)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 200 characters");
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplateId) is not null;
    }
}
