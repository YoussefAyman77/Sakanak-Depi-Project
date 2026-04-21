namespace Sakanak.Domain.Entities;

public class Student : User
{
    public string University { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public int LatePaymentCount { get; set; }
    public int? ApartmentGroupId { get; set; }
    public ApartmentGroup? ApartmentGroup { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public LifestyleQuestionnaire? Questionnaire { get; set; }
    public ICollection<Media> Media { get; set; } = new List<Media>();
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
