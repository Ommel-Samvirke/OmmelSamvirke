using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class ArchivePageTemplateCommandValidator : AbstractValidator<ArchivePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public ArchivePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;

        RuleFor(p => p.PageTemplateId)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template with id {PropertyValue} does not exist.");
    }
    
    private async Task<bool> PageMustExist(int id, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(id, cancellationToken) is not null;
    }
}
