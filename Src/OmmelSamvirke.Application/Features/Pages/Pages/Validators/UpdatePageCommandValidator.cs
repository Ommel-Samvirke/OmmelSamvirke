using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly ICommunityRepository _communityRepository;

    public UpdatePageCommandValidator(
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository,
        ICommunityRepository communityRepository
    )
    {
        _pageRepository = pageRepository;
        _pageTemplateRepository = pageTemplateRepository;
        _communityRepository = communityRepository;

        RuleFor(p => p.OriginalPage.Id)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
        
        RuleFor(p => p.OriginalPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters");

        RuleFor(p => p.UpdatedPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters")
            .MustAsync((p, _, cancellationToken) => NameMustBeUnique(p.UpdatedPage.Name, p.OriginalPage.Id, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Name must be unique");
        
        RuleFor(p => p.UpdatedPage.PageTemplateId)
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
        
        RuleFor(p => p.UpdatedContentBlockDataElements)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Updated content blocks must be set");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId, cancellationToken) is not null;
    }
    
    private async Task<bool> PageTemplateMustExist(int pageTemplateId, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplateId, cancellationToken) is not null;
    }
    
    private async Task<bool> NameMustBeUnique(string name, int pageId, CancellationToken cancellationToken)
    {
        Page originalPage = (await _pageRepository.GetByIdAsync(pageId, cancellationToken))!;
        IReadOnlyList<Page> pages = await _communityRepository.GetPages(originalPage.CommunityId, cancellationToken);
        return pages.All(p => p.Name != name);
    }
}
