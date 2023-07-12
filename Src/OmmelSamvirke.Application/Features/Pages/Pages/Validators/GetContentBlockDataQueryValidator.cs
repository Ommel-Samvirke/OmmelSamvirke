using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetContentBlockDataQueryValidator : AbstractValidator<GetContentBlockDataQuery>
{
    private readonly IPageRepository _pageRepository;

    public GetContentBlockDataQueryValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        
        RuleFor(x => x.PageId)
            .MustAsync(PageExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
    }
    
    private async Task<bool> PageExists(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
}
