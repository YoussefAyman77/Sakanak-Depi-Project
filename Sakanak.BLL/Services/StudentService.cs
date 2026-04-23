using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<Student>> GetStudentByIdAsync(int studentId)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId);
        if (student == null) return OperationResult<Student>.FailureResult("Student not found");
        return OperationResult<Student>.SuccessResult(student);
    }

    public async Task<OperationResult<IEnumerable<Student>>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.Students.GetAllAsync();
        return OperationResult<IEnumerable<Student>>.SuccessResult(students);
    }

    public async Task<OperationResult<bool>> SuspendStudentAsync(int studentId, int adminId, string reason)
    {
        try
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null) return OperationResult<bool>.FailureResult("Student not found");

            // Check for active contracts
            var activeContracts = await _unitOfWork.Contracts.GetActiveContractsAsync();
            if (activeContracts.Any(c => c.StudentId == studentId))
            {
                return OperationResult<bool>.FailureResult("Cannot suspend student with active contracts");
            }

            student.Status = UserStatus.Suspended;
            await _unitOfWork.SaveChangesAsync();
            
            _logger.LogInformation("Student {StudentId} suspended by admin {AdminId}. Reason: {Reason}", studentId, adminId, reason);
            
            return OperationResult<bool>.SuccessResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error suspending student");
            return OperationResult<bool>.FailureResult("An error occurred while suspending the student");
        }
    }

    public async Task<OperationResult<bool>> ReactivateStudentAsync(int studentId, int adminId)
    {
        try
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null) return OperationResult<bool>.FailureResult("Student not found");

            student.Status = UserStatus.Active;
            await _unitOfWork.SaveChangesAsync();
            
            _logger.LogInformation("Student {StudentId} reactivated by admin {AdminId}", studentId, adminId);
            
            return OperationResult<bool>.SuccessResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reactivating student");
            return OperationResult<bool>.FailureResult("An error occurred while reactivating the student");
        }
    }

    public async Task<OperationResult<IEnumerable<Student>>> GetStudentsWithLatePaymentsAsync()
    {
        var students = await _unitOfWork.Students.GetStudentsWithLatePaymentsAsync();
        return OperationResult<IEnumerable<Student>>.SuccessResult(students);
    }

    public async Task<OperationResult<IEnumerable<Student>>> GetStudentsWithoutGroupAsync()
    {
        var students = await _unitOfWork.Students.GetStudentsWithoutGroupAsync();
        return OperationResult<IEnumerable<Student>>.SuccessResult(students);
    }
}
