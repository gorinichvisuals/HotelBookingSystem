namespace HotelBooking.Application.Services.Abstractions;

public interface IRoomService
{
    Task<RoomsDTO> GetRooms(RoomFilterDTO filterDTO, CancellationToken cancellationToken);
}