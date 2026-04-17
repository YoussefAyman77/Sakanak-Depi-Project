using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IPaymentRepository
    {
        // CRUD
        Task<Payment?> GetByIdAsync(int id);
        Task<List<Payment>> GetAllAsync();
        Task AddAsync(Payment entity);
        void Update(Payment entity);
        void Delete(Payment entity);

        // Main operations
        Task<List<Payment>> GetPaymentsByStudentAsync(int studentId);
        Task<List<Payment>> GetPaymentsByLandlordAsync(int landlordId);
    }
}
