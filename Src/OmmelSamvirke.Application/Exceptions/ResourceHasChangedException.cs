namespace OmmelSamvirke.Application.Exceptions;

public class ResourceHasChangedException : Exception
{
    public ResourceHasChangedException(string message) : base(message)
    {
        
    }
}