namespace HotelBooking.Infrastructure.Repositories.Abstractions;

public interface IBookingRepository : IBaseRepository<Booking>
{
    Task CancelBooking(Expression<Func<Booking, bool>> predicateExpression);
    Task UpdateBooking(Expression<Func<Booking, bool>> predicateExpression,
        int roomId, decimal amount, DateTime checkInDate, DateTime checkOutDate);
}