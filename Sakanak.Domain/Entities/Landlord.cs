namespace Sakanak.Domain.Entities;

public class Landlord : User
{
    public bool VerificationStatus { get; set; }
    public int TotalProperties { get; set; }
    public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
