namespace HotelBooking.Infrastructure.Repositories.Implementations;

internal sealed class RoomPhotoRepository(BookingContext context) : IRoomPhotoRepository
{
    public BookingContext Context { get; set; } = context;
}