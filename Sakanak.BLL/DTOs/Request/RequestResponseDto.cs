using System;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.DTOs.Request;

public class RequestResponseDto
{
    public int RequestId { get; set; }
    public int LandlordId { get; set; }
    public string LandlordName { get; set; } = string.Empty;
    public int ApartmentId { get; set; }
    public string ApartmentName { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}
