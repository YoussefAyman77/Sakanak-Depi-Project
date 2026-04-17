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
    public class MessageRepository : IMessageRepository
    {
        private readonly SakanakDbContext _context;

        public MessageRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task AddAsync(Message entity)
        {
            await _context.Messages.AddAsync(entity);
        }

        public void Update(Message entity)
        {
            _context.Messages.Update(entity);
        }

        public void Delete(Message entity)
        {
            _context.Messages.Remove(entity);
        }

        public async Task<List<Message>> GetMessagesByChatAsync(int chatId)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChatID == chatId)
                .OrderBy(m => m.SentTime)
                .ToListAsync();
        }
    }
}
