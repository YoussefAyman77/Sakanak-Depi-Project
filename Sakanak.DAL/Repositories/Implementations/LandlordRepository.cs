using Microsoft.EntityFrameworkCore;
using Sakanak.DAL.Data;
using Sakanak.DAL.Repositories.Interfaces;
using Sakanak.Domain.Entities;

namespace Sakanak.DAL.Repositories.Implementations;

public class LandlordRepository : RepositoryBase<Landlord>, ILandlordRepository
{
    public LandlordRepository(SakanakDbContext context) : base(context)
    {
    }

    public async Task<Landlord?> GetByEmailAsync(string email)
        => await DbSet.FirstOrDefaultAsync(e => e.Email == email);

    public async Task<IEnumerable<Landlord>> GetVerifiedLandlordsAsync()
        => await DbSet.Where(e => e.VerificationStatus).ToListAsync();

    public async Task<Landlord?> GetLandlordWithApartmentsAsync(int landlordId)
        => await DbSet.Include(e => e.Apartments).FirstOrDefaultAsync(e => e.UserId == landlordId);

    public async Task<int> GetTotalPropertiesCountAsync(int landlordId)
        => await Context.Apartments.CountAsync(e => e.LandlordId == landlordId);
}
