using System;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.DTOs.Booking;

public class BookingResponseDto
{
    public int BookingId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int ApartmentId { get; set; }
    public string ApartmentName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime RequestedStartDate { get; set; }
    public DateTime RequestedEndDate { get; set; }
}
