using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IMessageRepository
    {
        // CRUD
        Task<Message?> GetByIdAsync(int id);
        Task<List<Message>> GetAllAsync();
        Task AddAsync(Message entity);
        void Update(Message entity);
        void Delete(Message entity);

        // Main operations
        Task<List<Message>> GetMessagesByChatAsync(int chatId);
    }
}
