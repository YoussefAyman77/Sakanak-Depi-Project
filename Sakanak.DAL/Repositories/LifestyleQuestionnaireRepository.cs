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
    public class LifestyleQuestionnaireRepository : ILifestyleQuestionnaireRepository
    {
        private readonly SakanakDbContext _context;

        public LifestyleQuestionnaireRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<LifestyleQuestionnaire?> GetByIdAsync(int id)
        {
            return await _context.LifestyleQuestionnaires.FindAsync(id);
        }

        public async Task<List<LifestyleQuestionnaire>> GetAllAsync()
        {
            return await _context.LifestyleQuestionnaires.ToListAsync();
        }

        public async Task AddAsync(LifestyleQuestionnaire entity)
        {
            await _context.LifestyleQuestionnaires.AddAsync(entity);
        }

        public void Update(LifestyleQuestionnaire entity)
        {
            _context.LifestyleQuestionnaires.Update(entity);
        }

        public void Delete(LifestyleQuestionnaire entity)
        {
            _context.LifestyleQuestionnaires.Remove(entity);
        }

        public async Task<LifestyleQuestionnaire?> GetByStudentIdAsync(int studentId)
        {
            return await _context.LifestyleQuestionnaires
                .FirstOrDefaultAsync(lq => lq.StudentID == studentId);
        }
    }
}
