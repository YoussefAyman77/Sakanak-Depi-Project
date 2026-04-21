using Microsoft.EntityFrameworkCore;
using Sakanak.DAL.Data;
using Sakanak.DAL.Repositories.Interfaces;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.DAL.Repositories.Implementations;

public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
{
    public AdminRepository(SakanakDbContext context) : base(context)
    {
    }

    public async Task<Admin?> GetByEmailAsync(string email)
        => await DbSet.FirstOrDefaultAsync(e => e.Email == email);

    public async Task<IEnumerable<Admin>> GetSuperAdminsAsync()
        => await DbSet.Where(e => e.RoleLevel == AdminRoleLevel.SuperAdmin).ToListAsync();
}
