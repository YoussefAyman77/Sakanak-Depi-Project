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
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly SakanakDbContext _context;

        public ApartmentRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Apartment?> GetByIdAsync(int id)
        {
            return await _context.Apartments.FindAsync(id);
        }

        public async Task<List<Apartment>> GetAllAsync()
        {
            return await _context.Apartments.ToListAsync();
        }

        public async Task AddAsync(Apartment entity)
        {
            await _context.Apartments.AddAsync(entity);
        }

        public void Update(Apartment entity)
        {
            _context.Apartments.Update(entity);
        }

        public void Delete(Apartment entity)
        {
            _context.Apartments.Remove(entity);
        }

        public async Task<List<Apartment>> GetApartmentsByCityAsync(string city)
        {
            return await _context.Apartments
                .Where(a => a.City == city)
                .ToListAsync();
        }

        public async Task<List<Apartment>> GetAvailableApartmentsAsync()
        {
            return await _context.Apartments
                .Where(a => a.AvailableSeats > 0)
                .ToListAsync();
        }

        public async Task<Apartment?> GetApartmentWithGroupAsync(int apartmentId)
        {
            return await _context.Apartments
                .Include(a => a.ApartmentGroup)
                    .ThenInclude(ag => ag!.Students)
                .FirstOrDefaultAsync(a => a.ApartmentID == apartmentId);
        }
    }
}
