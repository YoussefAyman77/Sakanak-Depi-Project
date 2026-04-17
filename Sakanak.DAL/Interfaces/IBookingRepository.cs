using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IBookingRepository
    {
        // CRUD
        Task<Booking?> GetByIdAsync(int id);
        Task<List<Booking>> GetAllAsync();
        Task AddAsync(Booking entity);
        void Update(Booking entity);
        void Delete(Booking entity);

        // Main operations
        Task<List<Booking>> GetBookingsByStudentAsync(int studentId);
        Task<List<Booking>> GetBookingsByApartmentAsync(int apartmentId);
    }
}
