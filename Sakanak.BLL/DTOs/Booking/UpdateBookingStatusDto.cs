using Sakanak.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Booking;

public class UpdateBookingStatusDto
{
    [Required]
    public BookingStatus Status { get; set; }
    
    public string? Reason { get; set; }
}
