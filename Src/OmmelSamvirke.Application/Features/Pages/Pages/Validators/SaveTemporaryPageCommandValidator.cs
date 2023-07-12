using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class SaveTemporaryPageCommandValidator : AbstractValidator<SaveTemporaryPageCommand>
{
    private readonly IPageRepository _pageRepository;

    public SaveTemporaryPageCommandValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        RuleFor(x => x.OriginalPage)
            .NotNull()
            .WithMessage("The original page cannot be null")
            .MustAsync(OriginalPageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("The original page does not exist");

        RuleFor(x => x.UpdatedPage)
            .NotNull()
            .WithMessage("The updated page cannot be null");
    }
    
    private async Task<bool> OriginalPageMustExist(PageDto originalPage, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(originalPage.Id) is not null;
    }
}
