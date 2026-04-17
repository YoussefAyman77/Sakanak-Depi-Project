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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SakanakDbContext _context;

        public PaymentRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task AddAsync(Payment entity)
        {
            await _context.Payments.AddAsync(entity);
        }

        public void Update(Payment entity)
        {
            _context.Payments.Update(entity);
        }

        public void Delete(Payment entity)
        {
            _context.Payments.Remove(entity);
        }

        public async Task<List<Payment>> GetPaymentsByStudentAsync(int studentId)
        {
            return await _context.Payments
                .Where(p => p.StudentID == studentId)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentsByLandlordAsync(int landlordId)
        {
            return await _context.Payments
                .Where(p => p.LandlordID == landlordId)
                .ToListAsync();
        }
    }
}
