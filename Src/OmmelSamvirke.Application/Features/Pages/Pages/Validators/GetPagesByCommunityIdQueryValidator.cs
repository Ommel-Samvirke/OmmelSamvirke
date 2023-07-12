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
        
        RuleFor(x => x.CommunityId)
            .MustAsync(CommunityExists)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist")
            .MustAsync(MustHavePages)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Community does not have any pages");
    }
    
    private async Task<bool> CommunityExists(int communityId, CancellationToken cancellationToken)
    {
        return await _communityRepository.GetByIdAsync(communityId) is not null;
    }
    
    private async Task<bool> MustHavePages(int communityId, CancellationToken cancellationToken)
    {
        return (await _communityRepository.GetPages(communityId)).Count > 0;
    }
}
