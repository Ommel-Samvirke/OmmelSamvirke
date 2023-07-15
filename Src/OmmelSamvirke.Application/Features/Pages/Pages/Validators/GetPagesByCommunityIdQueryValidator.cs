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
        
        
    }
}
