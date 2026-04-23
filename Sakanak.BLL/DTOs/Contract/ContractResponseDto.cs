using System;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.DTOs.Contract;

public class ContractResponseDto
{
    public int ContractId { get; set; }
    public int BookingId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int ApartmentId { get; set; }
    public string ApartmentName { get; set; } = string.Empty;
    public int LandlordId { get; set; }
    public string LandlordName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public ContractStatus Status { get; set; }
}
