using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Booking;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Exceptions;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BookingService> _logger;

    public BookingService(IUnitOfWork unitOfWork, ILogger<BookingService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<BookingResponseDto>> CreateBookingAsync(CreateBookingDto dto)
    {
        try
        {
            _logger.LogInformation("Creating booking for student {StudentId} and apartment {ApartmentId}", dto.StudentId, dto.ApartmentId);

            var apartment = await _unitOfWork.Apartments.GetByIdAsync(dto.ApartmentId);
            if (apartment == null)
            {
                return OperationResult<BookingResponseDto>.FailureResult("Apartment not found");
            }

            if (!apartment.IsActive)
            {
                return OperationResult<BookingResponseDto>.FailureResult("Apartment is not active");
            }

            var booking = new Booking
            {
                StudentId = dto.StudentId,
                ApartmentId = dto.ApartmentId,
                ApartmentGroupId = dto.ApartmentGroupId,
                BookingDate = DateTime.UtcNow,
                Status = BookingStatus.Pending,
                RequestedStartDate = dto.RequestedStartDate,
                RequestedEndDate = dto.RequestedEndDate
            };

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            var response = MapToResponseDto(booking);
            return OperationResult<BookingResponseDto>.SuccessResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating booking");
            return OperationResult<BookingResponseDto>.FailureResult("An error occurred while creating the booking");
        }
    }

    public async Task<OperationResult<bool>> AcceptBookingAsync(int bookingId, int landlordId)
    {
        return await UpdateBookingStatusAsync(bookingId, landlordId, BookingStatus.Accepted, true);
    }

    public async Task<OperationResult<bool>> RejectBookingAsync(int bookingId, int landlordId)
    {
        return await UpdateBookingStatusAsync(bookingId, landlordId, BookingStatus.Rejected, true);
    }

    public async Task<OperationResult<bool>> CancelBookingAsync(int bookingId, int studentId)
    {
        return await UpdateBookingStatusAsync(bookingId, studentId, BookingStatus.Cancelled, false);
    }

    public async Task<OperationResult<BookingResponseDto>> GetBookingByIdAsync(int bookingId)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null) return OperationResult<BookingResponseDto>.FailureResult("Booking not found");

        return OperationResult<BookingResponseDto>.SuccessResult(MapToResponseDto(booking));
    }

    public async Task<OperationResult<IEnumerable<BookingResponseDto>>> GetStudentBookingsAsync(int studentId)
    {
        var bookings = await _unitOfWork.Bookings.GetByStudentIdAsync(studentId);
        return OperationResult<IEnumerable<BookingResponseDto>>.SuccessResult(bookings.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<BookingResponseDto>>> GetLandlordBookingsAsync(int landlordId)
    {
        var bookings = await _unitOfWork.Bookings.GetBookingsByLandlordIdAsync(landlordId);
        return OperationResult<IEnumerable<BookingResponseDto>>.SuccessResult(bookings.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<BookingResponseDto>>> GetPendingBookingsAsync()
    {
        var bookings = await _unitOfWork.Bookings.GetPendingBookingsAsync();
        return OperationResult<IEnumerable<BookingResponseDto>>.SuccessResult(bookings.Select(MapToResponseDto));
    }

    private async Task<OperationResult<bool>> UpdateBookingStatusAsync(int bookingId, int userId, BookingStatus newStatus, bool isLandlord)
    {
        try
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null) throw new BookingNotFoundException(bookingId);

            if (isLandlord && booking.Apartment.LandlordId != userId)
            {
                return OperationResult<bool>.FailureResult("Unauthorized landlord action");
            }
            if (!isLandlord && booking.StudentId != userId)
            {
                return OperationResult<bool>.FailureResult("Unauthorized student action");
            }

            if (booking.Status != BookingStatus.Pending)
            {
                throw new InvalidBookingStatusException($"Cannot change status of booking from {booking.Status} to {newStatus}");
            }

            booking.Status = newStatus;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult<bool>.SuccessResult(true);
        }
        catch (BusinessRuleViolationException ex)
        {
            return OperationResult<bool>.FailureResult(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating booking status");
            return OperationResult<bool>.FailureResult("An error occurred while updating the booking status");
        }
    }

    private static BookingResponseDto MapToResponseDto(Booking booking)
    {
        return new BookingResponseDto
        {
            BookingId = booking.BookingId,
            StudentId = booking.StudentId,
            StudentName = booking.Student?.Name ?? "Unknown",
            ApartmentId = booking.ApartmentId,
            ApartmentName = booking.Apartment?.Address ?? "Unknown",
            BookingDate = booking.BookingDate,
            Status = booking.Status,
            RequestedStartDate = booking.RequestedStartDate,
            RequestedEndDate = booking.RequestedEndDate
        };
    }
}
