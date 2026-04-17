using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IApartmentGroupRepository
    {
        // CRUD
        Task<ApartmentGroup?> GetByIdAsync(int id);
        Task<List<ApartmentGroup>> GetAllAsync();
        Task AddAsync(ApartmentGroup entity);
        void Update(ApartmentGroup entity);
        void Delete(ApartmentGroup entity);

        // Main operations
        Task<ApartmentGroup?> GetGroupWithStudentsAsync(int groupId);
    }
}
