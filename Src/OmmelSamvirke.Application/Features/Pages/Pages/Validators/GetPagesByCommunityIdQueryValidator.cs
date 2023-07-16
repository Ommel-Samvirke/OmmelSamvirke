using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetPagesByCommunityIdQueryValidator : AbstractValidator<GetPagesByCommunityIdQuery>
{
    private readonly ICommunityRepository _communityRepository;

    public GetPagesByCommunityIdQueryValidator(ICommunityRepository communityRepository)
    {
        _communityRepository = communityRepository;
        
            RuleFor(p => p.CommunityId)
                .MustAsync(CommunityMustExist)
                .WithErrorCode(ErrorCode.ResourceNotFound)
                .WithMessage("Community does not exist");
    }
    
    private async Task<bool> CommunityMustExist(int communityId, CancellationToken cancellationToken)
    {
        return await _communityRepository.GetByIdAsync(communityId) is not null;
    }
}
