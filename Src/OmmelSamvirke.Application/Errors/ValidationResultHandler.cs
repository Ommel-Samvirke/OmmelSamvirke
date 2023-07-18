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
                    throw new ResourceInUseException(string.IsNullOrEmpty(error.ErrorMessage) ?
                        "Resource is in use" : error.ErrorMessage);
                case ErrorCode.ResourceNotFound:
                    throw new NotFoundException(string.IsNullOrEmpty(error.ErrorMessage) ?
                        "Resource not found" : error.ErrorMessage);
                case ErrorCode.ResourceHasChanged:
                    throw new ResourceHasChangedException(string.IsNullOrEmpty(error.ErrorMessage) ?
                        "Resource has changed" : error.ErrorMessage);
                case ErrorCode.BadRequest:
                    throw new BadRequestException(string.IsNullOrEmpty(error.ErrorMessage) ?
                        "Invalid request" : error.ErrorMessage);
            }
        });
            
        throw new BadRequestException("Invalid request", validationResult);
    }
}
