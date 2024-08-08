namespace HotelBooking.Application.Services.Abstractions;

public interface IBookingService
{
    Task<ApplicationResult<BookingDTO>> CreateBooking(BookingCreateDTO bookingCreateDTO, int customerId);
    Task<ApplicationResult<BookingDTO>> UpdateBooking(BookingUpdateDTO bookingUpdateDTO, int bookingId);
    Task<ApplicationResult> CancelBooking(int bookingId);
}