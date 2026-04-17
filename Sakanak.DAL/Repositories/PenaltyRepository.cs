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
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly SakanakDbContext _context;

        public PenaltyRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Penalty?> GetByIdAsync(int id)
        {
            return await _context.Penalties.FindAsync(id);
        }

        public async Task<List<Penalty>> GetAllAsync()
        {
            return await _context.Penalties.ToListAsync();
        }

        public async Task AddAsync(Penalty entity)
        {
            await _context.Penalties.AddAsync(entity);
        }

        public void Update(Penalty entity)
        {
            _context.Penalties.Update(entity);
        }

        public void Delete(Penalty entity)
        {
            _context.Penalties.Remove(entity);
        }

        public async Task<List<Penalty>> GetPenaltiesByStudentAsync(int studentId)
        {
            return await _context.Penalties
                .Include(p => p.IssuedByAdmin)
                .Where(p => p.StudentID == studentId)
                .OrderByDescending(p => p.IssuedAt)
                .ToListAsync();
        }
    }
}
