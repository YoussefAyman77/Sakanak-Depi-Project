using System;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Booking;

public class CreateBookingDto
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int ApartmentId { get; set; }

    [Required]
    public DateTime RequestedStartDate { get; set; }

    [Required]
    public DateTime RequestedEndDate { get; set; }

    public int? ApartmentGroupId { get; set; }
}
