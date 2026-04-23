using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.BLL.DTOs.Booking;
using Sakanak.BLL.DTOs.Common;

namespace Sakanak.BLL.Interfaces;

public interface IBookingService
{
    Task<OperationResult<BookingResponseDto>> CreateBookingAsync(CreateBookingDto dto);
    Task<OperationResult<bool>> AcceptBookingAsync(int bookingId, int landlordId);
    Task<OperationResult<bool>> RejectBookingAsync(int bookingId, int landlordId);
    Task<OperationResult<bool>> CancelBookingAsync(int bookingId, int studentId);
    Task<OperationResult<BookingResponseDto>> GetBookingByIdAsync(int bookingId);
    Task<OperationResult<IEnumerable<BookingResponseDto>>> GetStudentBookingsAsync(int studentId);
    Task<OperationResult<IEnumerable<BookingResponseDto>>> GetLandlordBookingsAsync(int landlordId);
    Task<OperationResult<IEnumerable<BookingResponseDto>>> GetPendingBookingsAsync();
}
