using Microsoft.EntityFrameworkCore;
using Sakanak.DAL.Contexts;
using Sakanak.DAL.Interfaces;
using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly SakanakDbContext _context;

        public BookingRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task AddAsync(Booking entity)
        {
            await _context.Bookings.AddAsync(entity);
        }

        public void Update(Booking entity)
        {
            _context.Bookings.Update(entity);
        }

        public void Delete(Booking entity)
        {
            _context.Bookings.Remove(entity);
        }

        public async Task<List<Booking>> GetBookingsByStudentAsync(int studentId)
        {
            return await _context.Bookings
                .Include(b => b.Apartment)
                .Where(b => b.StudentID == studentId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByApartmentAsync(int apartmentId)
        {
            return await _context.Bookings
                .Include(b => b.Student)
                .Where(b => b.ApartmentID == apartmentId)
                .ToListAsync();
        }
    }
}
