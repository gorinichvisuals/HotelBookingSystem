namespace HotelBooking.Infrastructure.Repositories.Implementations;

internal sealed class BookingRepository(BookingContext context) : IBookingRepository
{
    public BookingContext Context { get; set; } = context;

    public async Task CancelBooking(Expression<Func<Booking, bool>> predicateExpression)
    {
        await Context.Bookings.Where(predicateExpression)
            .ExecuteUpdateAsync(b => b.SetProperty(b => b.IsCancelled, true));
    }

    public async Task UpdateBooking(Expression<Func<Booking, bool>> predicateExpression, 
        int roomId,decimal amount, DateTime checkInDate, DateTime checkOutDate)
    {
        await Context.Bookings.Where(predicateExpression)
            .ExecuteUpdateAsync(b => 
            b.SetProperty(b => b.RoomId, roomId)
            .SetProperty(b => b.Amount, amount)
            .SetProperty(b => b.CheckInDate, checkInDate)
            .SetProperty(b => b.CheckOutDate, checkOutDate));
    }
}