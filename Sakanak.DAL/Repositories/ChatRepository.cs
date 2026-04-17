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
    public class ChatRepository : IChatRepository
    {
        private readonly SakanakDbContext _context;

        public ChatRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Chat?> GetByIdAsync(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public async Task<List<Chat>> GetAllAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task AddAsync(Chat entity)
        {
            await _context.Chats.AddAsync(entity);
        }

        public void Update(Chat entity)
        {
            _context.Chats.Update(entity);
        }

        public void Delete(Chat entity)
        {
            _context.Chats.Remove(entity);
        }

        public async Task<Chat?> GetChatWithMessagesAsync(int chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages.OrderBy(m => m.SentTime))
                .FirstOrDefaultAsync(c => c.ChatID == chatId);
        }
    }
}
