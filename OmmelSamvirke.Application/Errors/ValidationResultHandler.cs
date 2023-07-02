using FluentValidation.Results;
using MediatR;
using OmmelSamvirke.Application.Exceptions;

namespace OmmelSamvirke.Application.Errors;

public static class ValidationResultHandler
{
    public static void Handle<T>(ValidationResult validationResult, IRequest<T> request)
    {
        if (!validationResult.Errors.Any()) return;
        
        validationResult.Errors.ForEach(error =>
        {
            switch (error.ErrorCode)
            {
                case ErrorCode.ResourceInUse:
                    throw new ResourceInUseException("Resource is in use and cannot be deleted");
                case ErrorCode.ResourceNotFound:
                    throw new NotFoundException("Resource not found");
            }
        });
            
        throw new BadRequestException("Invalid PageTemplate request", validationResult);
    }
}
