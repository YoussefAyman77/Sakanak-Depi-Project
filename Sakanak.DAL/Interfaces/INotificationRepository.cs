using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface INotificationRepository
    {
        // CRUD
        Task<Notification?> GetByIdAsync(int id);
        Task<List<Notification>> GetAllAsync();
        Task AddAsync(Notification entity);
        void Update(Notification entity);
        void Delete(Notification entity);

        // Main operations
        Task<List<Notification>> GetNotificationsByUserAsync(int userId);
    }
}
