namespace Sakanak.BLL.Exceptions;

public class InvalidBookingStatusException : BusinessRuleViolationException
{
    public InvalidBookingStatusException(string message) : base(message) { }
}
