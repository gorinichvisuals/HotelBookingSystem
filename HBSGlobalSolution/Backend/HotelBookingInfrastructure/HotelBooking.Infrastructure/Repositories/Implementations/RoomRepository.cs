namespace HotelBooking.Infrastructure.Repositories.Implementations;

internal sealed class RoomRepository(BookingContext context) : IRoomRepository
{
    public BookingContext Context { get; set; } = context;

    public async Task<(ICollection<T>, int)> GetFilteredRooms<T>(Expression<Func<Room, T>> selectExpression, 
        int skipItems, int takeItems, bool ascending, string roomType, string roomClass, 
        int? floor, DateTime? checkInDate, DateTime? checkOutDate, CancellationToken cancellationToken)
    {
        IQueryable<Room> query = Context.Rooms.AsQueryable();

        if (!string.IsNullOrEmpty(roomType))
        {
            RoomType parsedType = Enum.Parse<RoomType>(roomType);

            query = query.Where(r => r.RoomType == parsedType);
        }

        if(!string.IsNullOrEmpty(roomClass))
        {
            RoomClass parsedClass = Enum.Parse<RoomClass>(roomClass);

            query = query.Where(r => r.RoomClass == parsedClass);
        }

        if(floor is not null)
            query = query.Where(r => r.Floor == floor);

        if (checkInDate is not null && checkOutDate is not null)
            query = query.Where(r => !r.Bookings!.Any(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate));

        query = ascending
            ? query.OrderBy(r => r.RoomNumber)
            : query.OrderByDescending(r => r.RoomNumber);

        var count = await query.CountAsync(cancellationToken);
        var rooms = await query
            .Skip(skipItems)
            .Take(takeItems)
            .Select(selectExpression)
            .ToListAsync(cancellationToken);

        return (rooms, count);
    }
}