using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IChatRepository
    {
        // CRUD
        Task<Chat?> GetByIdAsync(int id);
        Task<List<Chat>> GetAllAsync();
        Task AddAsync(Chat entity);
        void Update(Chat entity);
        void Delete(Chat entity);

        // Main operations
        Task<Chat?> GetChatWithMessagesAsync(int chatId);
    }
}
