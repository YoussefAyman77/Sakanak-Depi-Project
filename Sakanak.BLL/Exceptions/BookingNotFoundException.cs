namespace Sakanak.BLL.Exceptions;

public class BookingNotFoundException : BusinessRuleViolationException
{
    public BookingNotFoundException(int bookingId) 
        : base($"Booking with ID {bookingId} was not found.") { }
}
