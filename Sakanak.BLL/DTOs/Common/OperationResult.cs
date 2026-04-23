using System.Collections.Generic;

namespace Sakanak.BLL.DTOs.Common;

/// <summary>
/// A generic wrapper for service operation results, including success status, data, and error messages.
/// </summary>
/// <typeparam name="T">The type of data returned by the operation.</typeparam>
public class OperationResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string> ValidationErrors { get; set; } = new();

    public static OperationResult<T> SuccessResult(T data) => new() { Success = true, Data = data };
    public static OperationResult<T> FailureResult(string message) => new() { Success = false, ErrorMessage = message };
    public static OperationResult<T> ValidationErrorResult(List<string> errors) => new() { Success = false, ValidationErrors = errors };
}
