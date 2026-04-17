using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IPenaltyRepository
    {
        // CRUD
        Task<Penalty?> GetByIdAsync(int id);
        Task<List<Penalty>> GetAllAsync();
        Task AddAsync(Penalty entity);
        void Update(Penalty entity);
        void Delete(Penalty entity);

        // Main operations
        Task<List<Penalty>> GetPenaltiesByStudentAsync(int studentId);
    }
}
