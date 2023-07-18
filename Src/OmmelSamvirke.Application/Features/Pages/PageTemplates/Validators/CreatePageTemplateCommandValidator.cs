using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateCommandValidator : AbstractValidator<CreatePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public CreatePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        
        RuleFor(p => p.PageTemplateCreateDto.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(225)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 200 characters")
            .MustAsync(NameMustBeUnique)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Template Name must be unique");
        
        RuleFor(p => p.PageTemplateCreateDto.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");
    }
    
    private async Task<bool> NameMustBeUnique(string name, CancellationToken cancellationToken)
    {
        IReadOnlyList<PageTemplate> pageTemplates = await _pageTemplateRepository.GetAsync(cancellationToken);
        return pageTemplates.All(p => p.Name != name);
    }
}
