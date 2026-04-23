using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IUnitOfWork unitOfWork, ILogger<AdminService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<Admin>> GetAdminByIdAsync(int adminId)
    {
        var admin = await _unitOfWork.Admins.GetByIdAsync(adminId);
        if (admin == null) return OperationResult<Admin>.FailureResult("Admin not found");
        return OperationResult<Admin>.SuccessResult(admin);
    }

    public async Task<OperationResult<IEnumerable<Admin>>> GetAllAdminsAsync()
    {
        var admins = await _unitOfWork.Admins.GetAllAsync();
        return OperationResult<IEnumerable<Admin>>.SuccessResult(admins);
    }

    public async Task<OperationResult<IDictionary<string, object>>> GetDashboardStatsAsync()
    {
        try
        {
            var stats = new Dictionary<string, object>();

            stats["TotalStudents"] = (await _unitOfWork.Students.GetAllAsync()).Count();
            stats["TotalLandlords"] = (await _unitOfWork.Landlords.GetAllAsync()).Count();
            stats["TotalApartments"] = (await _unitOfWork.Apartments.GetAllAsync()).Count();
            
            stats["PendingRequests"] = (await _unitOfWork.Requests.GetPendingApartmentRequestsAsync()).Count();
            stats["PendingContracts"] = (await _unitOfWork.Contracts.GetPendingApprovalContractsAsync()).Count();
            stats["PendingPayments"] = (await _unitOfWork.Payments.GetPendingPaymentsAsync()).Count();

            return OperationResult<IDictionary<string, object>>.SuccessResult(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard stats");
            return OperationResult<IDictionary<string, object>>.FailureResult("An error occurred while retrieving dashboard statistics");
        }
    }

    public async Task<OperationResult<int>> GetPendingRequestsCountAsync()
    {
        var count = (await _unitOfWork.Requests.GetPendingApartmentRequestsAsync()).Count();
        return OperationResult<int>.SuccessResult(count);
    }

    public async Task<OperationResult<int>> GetPendingContractsCountAsync()
    {
        var count = (await _unitOfWork.Contracts.GetPendingApprovalContractsAsync()).Count();
        return OperationResult<int>.SuccessResult(count);
    }
}
