using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class RemoveContentBlockFromPageTemplateCommandValidator : AbstractValidator<RemoveContentBlockFromPageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;

    public RemoveContentBlockFromPageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository, IContentBlockRepository contentBlockRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;

        RuleFor(x => x.ContentBlock)
            .NotNull()
            .WithMessage("Content block cannot be null.")
            .MustAsync(ContentBlockMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Content block does not exist.");

        RuleFor(x => x.PageTemplate)
            .NotNull()
            .WithMessage("Page template cannot be null.")
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist.")
            .MustAsync(MustContainContentBlock)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page template does not contain the provided content block.");
    }
    
    private async Task<bool> ContentBlockMustExist(ContentBlock? contentBlock, CancellationToken cancellationToken)
    {
        if (contentBlock is null) return false;
        return await _contentBlockRepository.GetByIdAsync((int)contentBlock.Id!) is not null;
    }
    
    private async Task<bool> PageTemplateMustExist(PageTemplate pageTemplate, CancellationToken cancellationToken)
    {
        if (pageTemplate.Id is null) return false;
        return await _pageTemplateRepository.GetByIdAsync((int)pageTemplate.Id!) is not null;
    }

    private async Task<bool> MustContainContentBlock(RemoveContentBlockFromPageTemplateCommand command, PageTemplate pageTemplate, CancellationToken cancellationToken)
    {
        if (pageTemplate.Id is null) return false;
        PageTemplate pageTemplateFromDb = (await _pageTemplateRepository.GetByIdAsync((int)pageTemplate.Id!))!;
        return pageTemplateFromDb.Blocks.Contains(command.ContentBlock);
    }
}
