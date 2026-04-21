using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.DAL.Repositories.Interfaces;

/// <summary>
/// Provides data access operations for request records.
/// </summary>
public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAllAsync();
    Task<Request?> GetByIdAsync(int id);
    Task AddAsync(Request entity);
    Task UpdateAsync(Request entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<Request>> GetByLandlordIdAsync(int landlordId);
    Task<IEnumerable<Request>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<Request>> GetByStatusAsync(RequestStatus status);
    Task<IEnumerable<Request>> GetByTypeAsync(RequestType type);
    Task<IEnumerable<Request>> GetPendingApartmentRequestsAsync();
    Task<IEnumerable<Request>> GetPendingContractRequestsAsync();
    Task<Request?> GetByApartmentIdAsync(int apartmentId);
    Task<Request?> GetByContractIdAsync(int contractId);
}
