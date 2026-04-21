using Sakanak.Domain.Enums;

namespace Sakanak.Domain.Entities;

public class Contract
{
    public int ContractId { get; set; }
    public int BookingId { get; set; }
    public int ApartmentId { get; set; }
    public int LandlordId { get; set; }
    public int? VerifiedByAdminId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public Booking Booking { get; set; } = null!;
    public Apartment Apartment { get; set; } = null!;
    public Landlord Landlord { get; set; } = null!;
    public Admin? VerifiedByAdmin { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
