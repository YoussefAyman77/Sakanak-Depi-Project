namespace Sakanak.BLL.Exceptions;

public class InvalidPaymentStatusException : BusinessRuleViolationException
{
    public InvalidPaymentStatusException(string message) : base(message) { }
}
