using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetPageQueryValidator : AbstractValidator<GetPageQuery>
{
    private readonly IPageRepository _pageRepository;

    public GetPageQueryValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        
        RuleFor(p => p.PageId)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
}
