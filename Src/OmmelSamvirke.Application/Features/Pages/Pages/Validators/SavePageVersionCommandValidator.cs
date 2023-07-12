using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class SavePageVersionCommandValidator : AbstractValidator<SavePageVersionCommand>
{
    private readonly IPageRepository _pageRepository;

    public SavePageVersionCommandValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        RuleFor(x => x.PageVersion)
            .NotNull()
            .WithMessage("Page version cannot be null")
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page version does not exist");
    }
    
    private async Task<bool> PageMustExist(PageDto pageVersion, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageVersion.Id) is not null;
    }
}
