namespace Sakanak.BLL.Exceptions;

public class InvalidContractStatusException : BusinessRuleViolationException
{
    public InvalidContractStatusException(string message) : base(message) { }
}
