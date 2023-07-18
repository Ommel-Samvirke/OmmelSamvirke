using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class CreatePageFromTemplateCommandValidator : AbstractValidator<CreatePageFromTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly ICommunityRepository _communityRepository;

    public CreatePageFromTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository, ICommunityRepository communityRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        _communityRepository = communityRepository;

        RuleFor(p => p.PageTemplateId)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
        
        RuleFor(p => p.PageName)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 200 characters")
            .MustAsync((p, _, cancellationToken) => NameMustBeUnique(p.PageName, p.CommunityId, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Name must be unique");
        
        RuleFor(p => p.CommunityId)
            .MustAsync(CommunityMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist");
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplateId, cancellationToken) is not null;
    }
    
    private async Task<bool> CommunityMustExist(int communityId, CancellationToken cancellationToken)
    {
        return await _communityRepository.GetByIdAsync(communityId, cancellationToken) is not null;
    }
    
    private async Task<bool> NameMustBeUnique(string name, int communityId, CancellationToken cancellationToken)
    {
        IReadOnlyList<Page> pages = await _communityRepository.GetPages(communityId, cancellationToken);
        return pages.All(p => p.Name != name);
    }
}
