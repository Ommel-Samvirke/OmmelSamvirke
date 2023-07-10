using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class DeletePageCommandValidator : AbstractValidator<DeletePageCommand>
{
    private readonly IPageRepository _pageRepository;

    public DeletePageCommandValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        RuleFor(x => x.Page)
            .NotNull()
            .WithMessage("Page must be set")
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
    }
    
    private async Task<bool> PageMustExist(PageDto page, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(page.Id) is not null;
    }
}
