using Sakanak.Domain.Enums;

namespace Sakanak.Domain.Entities;

public class Booking
{
    public int BookingId { get; set; }
    public int StudentId { get; set; }
    public int ApartmentId { get; set; }
    public int? ApartmentGroupId { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime RequestedStartDate { get; set; }
    public DateTime RequestedEndDate { get; set; }
    public Student Student { get; set; } = null!;
    public Apartment Apartment { get; set; } = null!;
    public ApartmentGroup? ApartmentGroup { get; set; }
    public Contract? Contract { get; set; }
}
