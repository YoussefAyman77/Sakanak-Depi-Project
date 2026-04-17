using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IAdminRepository
    {
        // CRUD
        Task<Admin?> GetByIdAsync(int id);
        Task<List<Admin>> GetAllAsync();
        Task AddAsync(Admin entity);
        void Update(Admin entity);
        void Delete(Admin entity);
    }
}
