namespace HotelBooking.Infrastructure.Repositories.Implementations;

internal sealed class AdminRepository(BookingContext context) : IAdminRepository
{
    public BookingContext Context { get; set; } = context;
}