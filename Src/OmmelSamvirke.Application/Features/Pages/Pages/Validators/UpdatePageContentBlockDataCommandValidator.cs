using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageContentBlockDataCommandValidator : AbstractValidator<UpdatePageContentBlockDataCommand>
{
    private readonly IContentBlockDataRepository _contentBlockDataRepository;

    public UpdatePageContentBlockDataCommandValidator(IContentBlockDataRepository contentBlockDataRepository)
    {
        _contentBlockDataRepository = contentBlockDataRepository;
        
        RuleFor(x => x.OriginalState)
            .MustAsync(ValidateOriginalState)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("The original state is not valid.");
        
        RuleFor(x => x.UpdatedState)
            .NotNull()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("The updated state cannot be null.");
    }
    
    private async Task<bool> ValidateOriginalState(List<IContentBlockData> originalState, CancellationToken cancellationToken)
    {
        foreach (IContentBlockData contentBlockData in originalState)
        {
            if (contentBlockData.BaseContentBlock.Id is null or 0)
            {
                return false;
            }
            
            if (contentBlockData.PageId == 0)
            {
                return false;
            }
        }
        
        List<IContentBlockData> savedState = await _contentBlockDataRepository.GetByPageIdAsync(originalState.First().PageId);
        return savedState.Equals(originalState);
    }
}
