using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
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
    
    private async Task<bool> ContentBlockMustExist(ContentBlockDto contentBlock, CancellationToken cancellationToken)
    {
        return await _contentBlockRepository.GetByIdAsync(contentBlock.Id) is not null;
    }
    
    private async Task<bool> PageTemplateMustExist(PageTemplateDto pageTemplate, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplate.Id) is not null;
    }

    private async Task<bool> MustContainContentBlock(RemoveContentBlockFromPageTemplateCommand command, PageTemplateDto pageTemplate, CancellationToken cancellationToken)
    {
        PageTemplate pageTemplateFromDb = (await _pageTemplateRepository.GetByIdAsync(pageTemplate.Id))!;
        ContentBlock contentBlock = (await _contentBlockRepository.GetByIdAsync(command.ContentBlock.Id))!;
        return pageTemplateFromDb.ContentBlocks.Contains(contentBlock);
    }
}
