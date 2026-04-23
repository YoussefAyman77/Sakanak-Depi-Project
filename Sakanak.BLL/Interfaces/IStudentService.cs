using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.DAL.Repositories.Interfaces; // Assuming Student might need a DTO or just the entity for now
using Sakanak.BLL.DTOs.Common;
using Sakanak.Domain.Entities;

namespace Sakanak.BLL.Interfaces;

public interface IStudentService
{
    Task<OperationResult<Student>> GetStudentByIdAsync(int studentId);
    Task<OperationResult<IEnumerable<Student>>> GetAllStudentsAsync();
    Task<OperationResult<bool>> SuspendStudentAsync(int studentId, int adminId, string reason);
    Task<OperationResult<bool>> ReactivateStudentAsync(int studentId, int adminId);
    Task<OperationResult<IEnumerable<Student>>> GetStudentsWithLatePaymentsAsync();
    Task<OperationResult<IEnumerable<Student>>> GetStudentsWithoutGroupAsync();
}
