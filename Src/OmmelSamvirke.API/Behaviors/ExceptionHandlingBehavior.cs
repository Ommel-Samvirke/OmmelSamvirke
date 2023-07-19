using System.Net;
using MediatR;
using OmmelSamvirke.API.Exceptions;
using OmmelSamvirke.Application.Exceptions;

namespace OmmelSamvirke.API.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (BadRequestException ex)
        {
            throw new RequestException(ex.Message, HttpStatusCode.BadRequest);
        }
        catch (NotFoundException ex)
        {
            throw new RequestException(ex.Message, HttpStatusCode.NotFound);
        }
        catch (ResourceHasChangedException ex)
        {
            throw new RequestException(ex.Message, HttpStatusCode.Conflict);
        }
        catch (ResourceInUseException ex)
        {
            throw new RequestException(ex.Message, HttpStatusCode.Conflict);
        }
        catch (Exception ex)
        {
            throw new RequestException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}
