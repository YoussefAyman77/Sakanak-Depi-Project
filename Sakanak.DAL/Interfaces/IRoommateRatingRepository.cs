using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Interfaces
{
    public interface IRoommateRatingRepository
    {
        // CRUD
        Task<RoommateRating?> GetByIdAsync(int id);
        Task<List<RoommateRating>> GetAllAsync();
        Task AddAsync(RoommateRating entity);
        void Update(RoommateRating entity);
        void Delete(RoommateRating entity);

        // Main operations
        Task<List<RoommateRating>> GetRatingsByStudentAsync(int studentId);
        Task<float> GetAverageRatingAsync(int studentId);
    }
}
