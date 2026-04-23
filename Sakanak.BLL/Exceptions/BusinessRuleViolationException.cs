using System;

namespace Sakanak.BLL.Exceptions;

/// <summary>
/// Base exception for business rule violations in the Sakanak system.
/// </summary>
public class BusinessRuleViolationException : Exception
{
    public BusinessRuleViolationException(string message) : base(message) { }
}
