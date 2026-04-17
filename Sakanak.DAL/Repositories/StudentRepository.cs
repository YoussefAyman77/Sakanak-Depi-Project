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
    public class StudentRepository : IStudentRepository
    {
        private readonly SakanakDbContext _context;

        public StudentRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task AddAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(entity);
        }

        public async Task<Student?> GetStudentWithQuestionnaireAsync(int studentId)
        {
            return await _context.Students
                .Include(s => s.LifestyleQuestionnaire)
                .FirstOrDefaultAsync(s => s.UserID == studentId);
        }

        public async Task<List<Student>> GetStudentsInGroupAsync(int groupId)
        {
            return await _context.Students
                .Where(s => s.ApartmentGroupID == groupId)
                .ToListAsync();
        }
    }
}
