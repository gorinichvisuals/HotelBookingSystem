namespace HotelBooking.Infrastructure.Repositories.Abstractions;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<(ICollection<T>, int)> GetFilteredRooms<T>(Expression<Func<Room, T>> selectExpression, int skipItems, int takeItems, bool ascending, 
        string roomType, string roomClass, int? floor, DateTime? checkInDate, DateTime? checkOutDate, CancellationToken cancellationToken);
}