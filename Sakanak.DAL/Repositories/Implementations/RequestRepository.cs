using Microsoft.EntityFrameworkCore;
using Sakanak.DAL.Data;
using Sakanak.DAL.Repositories.Interfaces;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.DAL.Repositories.Implementations;

public class RequestRepository : RepositoryBase<Request>, IRequestRepository
{
    public RequestRepository(SakanakDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Request>> GetByLandlordIdAsync(int landlordId)
        => await DbSet.Where(e => e.LandlordId == landlordId).ToListAsync();

    public async Task<IEnumerable<Request>> GetByStudentIdAsync(int studentId)
        => await DbSet.Where(e => e.StudentId == studentId).ToListAsync();

    public async Task<IEnumerable<Request>> GetByStatusAsync(RequestStatus status)
        => await DbSet.Where(e => e.Status == status).ToListAsync();

    public async Task<IEnumerable<Request>> GetByTypeAsync(RequestType type)
        => await DbSet.Where(e => e.Type == type).ToListAsync();

    public async Task<IEnumerable<Request>> GetPendingApartmentRequestsAsync()
        => await DbSet.Where(e => e.Type == RequestType.Apartment && e.Status == RequestStatus.Pending).ToListAsync();

    public async Task<IEnumerable<Request>> GetPendingContractRequestsAsync()
        => await DbSet.Where(e => e.Type == RequestType.Contract && e.Status == RequestStatus.Pending).ToListAsync();

    public async Task<Request?> GetByApartmentIdAsync(int apartmentId)
        => await DbSet
            .Where(e => e.ApartmentId == apartmentId)
            .OrderByDescending(e => e.CreatedAt)
            .FirstOrDefaultAsync();

    public async Task<Request?> GetByContractIdAsync(int contractId)
        => await DbSet
            .Where(e => e.ContractId == contractId)
            .OrderByDescending(e => e.CreatedAt)
            .FirstOrDefaultAsync();
}
