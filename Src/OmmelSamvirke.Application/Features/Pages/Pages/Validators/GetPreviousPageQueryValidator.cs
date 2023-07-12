using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetPreviousPageQueryValidator : AbstractValidator<GetPreviousPageQuery>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IPageRepository _pageRepository;

    public GetPreviousPageQueryValidator(ICommunityRepository communityRepository, IPageRepository pageRepository)
    {
        _communityRepository = communityRepository;
        _pageRepository = pageRepository;
        
        RuleFor(x => x.CommunityId)
            .MustAsync(CommunityExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist");
        
        RuleFor(x => x.CurrentPageId)
            .MustAsync(PageExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
    }
    
    private async Task<bool> CommunityExists(int communityId, CancellationToken cancellationToken)
    {
        return await _communityRepository.GetByIdAsync(communityId) is not null;
    }
    
    private async Task<bool> PageExists(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
}
