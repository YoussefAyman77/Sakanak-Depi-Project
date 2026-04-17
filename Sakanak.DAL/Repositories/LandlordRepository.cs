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
    public class LandlordRepository : ILandlordRepository
    {
        private readonly SakanakDbContext _context;

        public LandlordRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Landlord?> GetByIdAsync(int id)
        {
            return await _context.Landlords.FindAsync(id);
        }

        public async Task<List<Landlord>> GetAllAsync()
        {
            return await _context.Landlords.ToListAsync();
        }

        public async Task AddAsync(Landlord entity)
        {
            await _context.Landlords.AddAsync(entity);
        }

        public void Update(Landlord entity)
        {
            _context.Landlords.Update(entity);
        }

        public void Delete(Landlord entity)
        {
            _context.Landlords.Remove(entity);
        }

        public async Task<Landlord?> GetLandlordWithApartmentsAsync(int landlordId)
        {
            return await _context.Landlords
                .Include(l => l.Apartments)
                .FirstOrDefaultAsync(l => l.UserID == landlordId);
        }
    }
}
