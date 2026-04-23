namespace Sakanak.BLL.Exceptions;

public class PaymentNotFoundException : BusinessRuleViolationException
{
    public PaymentNotFoundException(int paymentId) 
        : base($"Payment with ID {paymentId} was not found.") { }
}
