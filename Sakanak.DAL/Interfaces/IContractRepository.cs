using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IContractRepository
    {
        // CRUD
        Task<Contract?> GetByIdAsync(int id);
        Task<List<Contract>> GetAllAsync();
        Task AddAsync(Contract entity);
        void Update(Contract entity);
        void Delete(Contract entity);

        // Main operations
        Task<Contract?> GetContractWithStudentsAsync(int contractId);
        Task<List<Contract>> GetActiveContractsAsync();
    }
}
