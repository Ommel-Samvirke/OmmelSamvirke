using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetPagesByCommunityIdQueryValidator : AbstractValidator<GetPagesByCommunityIdQuery>
{
    public GetPagesByCommunityIdQueryValidator(ICommunityRepository communityRepository)
    {
        RuleFor(p => p.CommunityId)
            .MustAsync(communityRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist");
    }
}
