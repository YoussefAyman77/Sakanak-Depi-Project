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
    public class RoommateRatingRepository : IRoommateRatingRepository
    {
        private readonly SakanakDbContext _context;

        public RoommateRatingRepository(SakanakDbContext context)
        {
            _context = context;
        }

        public async Task<RoommateRating?> GetByIdAsync(int id)
        {
            return await _context.RoommateRatings.FindAsync(id);
        }

        public async Task<List<RoommateRating>> GetAllAsync()
        {
            return await _context.RoommateRatings.ToListAsync();
        }

        public async Task AddAsync(RoommateRating entity)
        {
            await _context.RoommateRatings.AddAsync(entity);
        }

        public void Update(RoommateRating entity)
        {
            _context.RoommateRatings.Update(entity);
        }

        public void Delete(RoommateRating entity)
        {
            _context.RoommateRatings.Remove(entity);
        }

        public async Task<List<RoommateRating>> GetRatingsByStudentAsync(int studentId)
        {
            return await _context.RoommateRatings
                .Include(rr => rr.RaterStudent)
                .Where(rr => rr.RatedStudentID == studentId)
                .ToListAsync();
        }

        public async Task<float> GetAverageRatingAsync(int studentId)
        {
            var ratings = await _context.RoommateRatings
                .Where(rr => rr.RatedStudentID == studentId)
                .Select(rr => rr.Score)
                .ToListAsync();

            return ratings.Any() ? (float)ratings.Average() : 0f;
        }
    }
}
