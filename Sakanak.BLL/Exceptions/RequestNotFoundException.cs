namespace Sakanak.BLL.Exceptions;

public class RequestNotFoundException : BusinessRuleViolationException
{
    public RequestNotFoundException(int requestId) 
        : base($"Landlord upload request with ID {requestId} was not found.") { }
}
