using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IApartmentRepository
    {
        // CRUD
        Task<Apartment?> GetByIdAsync(int id);
        Task<List<Apartment>> GetAllAsync();
        Task AddAsync(Apartment entity);
        void Update(Apartment entity);
        void Delete(Apartment entity);

        // Main operations
        Task<List<Apartment>> GetApartmentsByCityAsync(string city);
        Task<List<Apartment>> GetAvailableApartmentsAsync();
        Task<Apartment?> GetApartmentWithGroupAsync(int apartmentId);
    }
}
