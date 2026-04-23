namespace Sakanak.BLL.Exceptions;

public class ContractNotFoundException : BusinessRuleViolationException
{
    public ContractNotFoundException(int contractId) 
        : base($"Contract with ID {contractId} was not found.") { }
}
