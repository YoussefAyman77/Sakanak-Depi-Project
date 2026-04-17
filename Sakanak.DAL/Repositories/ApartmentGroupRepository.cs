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
    public class ApartmentGroupRepository : IApartmentGroupRepository
    {
        private readonly SakanakDbContext _context;

        public ApartmentGroupRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<ApartmentGroup?> GetByIdAsync(int id)
        {
            return await _context.ApartmentGroups.FindAsync(id);
        }

        public async Task<List<ApartmentGroup>> GetAllAsync()
        {
            return await _context.ApartmentGroups.ToListAsync();
        }

        public async Task AddAsync(ApartmentGroup entity)
        {
            await _context.ApartmentGroups.AddAsync(entity);
        }

        public void Update(ApartmentGroup entity)
        {
            _context.ApartmentGroups.Update(entity);
        }

        public void Delete(ApartmentGroup entity)
        {
            _context.ApartmentGroups.Remove(entity);
        }

        public async Task<ApartmentGroup?> GetGroupWithStudentsAsync(int groupId)
        {
            return await _context.ApartmentGroups
                .Include(ag => ag.Students)
                .FirstOrDefaultAsync(ag => ag.GroupID == groupId);
        }
    }
}
