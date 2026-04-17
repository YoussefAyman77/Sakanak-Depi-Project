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
    public class NotificationRepository : INotificationRepository
    {
        private readonly SakanakDbContext _context;

        public NotificationRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task AddAsync(Notification entity)
        {
            await _context.Notifications.AddAsync(entity);
        }

        public void Update(Notification entity)
        {
            _context.Notifications.Update(entity);
        }

        public void Delete(Notification entity)
        {
            _context.Notifications.Remove(entity);
        }

        public async Task<List<Notification>> GetNotificationsByUserAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
