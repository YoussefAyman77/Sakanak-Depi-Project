using Microsoft.EntityFrameworkCore;
using Sakanak.DAL.Contexts;
using Sakanak.DAL.Interfaces;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly SakanakDbContext _context;

        public ContractRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await _context.Contracts.FindAsync(id);
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task AddAsync(Contract entity)
        {
            await _context.Contracts.AddAsync(entity);
        }

        public void Update(Contract entity)
        {
            _context.Contracts.Update(entity);
        }

        public void Delete(Contract entity)
        {
            _context.Contracts.Remove(entity);
        }

        public async Task<Contract?> GetContractWithStudentsAsync(int contractId)
        {
            return await _context.Contracts
                .Include(c => c.Students)
                .Include(c => c.Apartment)
                .FirstOrDefaultAsync(c => c.ContractID == contractId);
        }

        public async Task<List<Contract>> GetActiveContractsAsync()
        {
            return await _context.Contracts
                .Include(c => c.Students)
                .Where(c => c.ContractStatus == ContractStatus.Active)
                .ToListAsync();
        }
    }
}
