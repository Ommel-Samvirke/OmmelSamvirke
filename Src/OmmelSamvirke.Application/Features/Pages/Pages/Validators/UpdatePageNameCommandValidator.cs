using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageNameCommandValidator : AbstractValidator<UpdatePageNameCommand>
{
    private readonly IPageRepository _pageRepository;

    public UpdatePageNameCommandValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        RuleFor(x => x.PageId)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
        
        RuleFor(x => x.PageName)
            .NotNull()
            .WithMessage("Page name must be set")
            .Must(x => x.Length > 3)
            .WithMessage("Page name must be longer than 3 characters");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
}
