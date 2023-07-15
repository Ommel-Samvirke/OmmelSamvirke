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
        
    }
    
}
