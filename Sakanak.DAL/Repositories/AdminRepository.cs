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
    public class AdminRepository : IAdminRepository
    {
        private readonly SakanakDbContext _context;

        public AdminRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task AddAsync(Admin entity)
        {
            await _context.Admins.AddAsync(entity);
        }

        public void Update(Admin entity)
        {
            _context.Admins.Update(entity);
        }

        public void Delete(Admin entity)
        {
            _context.Admins.Remove(entity);
        }
    }
}
