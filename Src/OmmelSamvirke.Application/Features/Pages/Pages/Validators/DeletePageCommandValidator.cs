using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class DeletePageCommandValidator : AbstractValidator<DeletePageCommand>
{
    public DeletePageCommandValidator(IPageRepository pageRepository)
    {
        RuleFor(p => p.PageId)
            .MustAsync(pageRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
    }
}
