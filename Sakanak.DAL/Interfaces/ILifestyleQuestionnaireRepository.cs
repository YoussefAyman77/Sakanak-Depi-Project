using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface ILifestyleQuestionnaireRepository
    {
        // CRUD
        Task<LifestyleQuestionnaire?> GetByIdAsync(int id);
        Task<List<LifestyleQuestionnaire>> GetAllAsync();
        Task AddAsync(LifestyleQuestionnaire entity);
        void Update(LifestyleQuestionnaire entity);
        void Delete(LifestyleQuestionnaire entity);

        // Main operations
        Task<LifestyleQuestionnaire?> GetByStudentIdAsync(int studentId);
    }
}
