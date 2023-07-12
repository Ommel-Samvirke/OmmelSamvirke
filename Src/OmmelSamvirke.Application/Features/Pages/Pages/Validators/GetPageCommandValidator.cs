using FluentValidation;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetPageCommandValidator : AbstractValidator<GetPageCommand>
{
    private readonly IPageRepository _pageRepository;

    public GetPageCommandValidator(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
        
        RuleFor(x => x.PageId)
            .MustAsync(PageExists)
            .WithMessage("Page does not exist");
    }
    
    private async Task<bool> PageExists(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
}
