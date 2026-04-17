using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface ILandlordRepository
    {
        // CRUD
        Task<Landlord?> GetByIdAsync(int id);
        Task<List<Landlord>> GetAllAsync();
        Task AddAsync(Landlord entity);
        void Update(Landlord entity);
        void Delete(Landlord entity);

        // Main operations
        Task<Landlord?> GetLandlordWithApartmentsAsync(int landlordId);
    }
}
