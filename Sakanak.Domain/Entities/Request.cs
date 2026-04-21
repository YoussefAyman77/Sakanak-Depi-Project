using Sakanak.Domain.Enums;

namespace Sakanak.Domain.Entities;

public class Request
{
    public int RequestId { get; set; }
    public int? LandlordId { get; set; }
    public int? StudentId { get; set; }
    public int? ApartmentId { get; set; }
    public int? ContractId { get; set; }
    public RequestType Type { get; set; }
    public RequestStatus Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public Landlord? Landlord { get; set; }
    public Student? Student { get; set; }
    public Apartment? Apartment { get; set; }
    public Contract? Contract { get; set; }
}
