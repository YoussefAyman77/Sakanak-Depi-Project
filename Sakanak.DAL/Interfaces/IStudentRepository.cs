using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IStudentRepository
    {
        // CRUD
        Task<Student?> GetByIdAsync(int id);
        Task<List<Student>> GetAllAsync();
        Task AddAsync(Student entity);
        void Update(Student entity);
        void Delete(Student entity);

        // Main operations
        Task<Student?> GetStudentWithQuestionnaireAsync(int studentId);
        Task<List<Student>> GetStudentsInGroupAsync(int groupId);
    }
}
