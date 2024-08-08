namespace HotelBooking.Application.DTO.Room;

public sealed record RoomsDTO(ICollection<RoomDTO> Rooms, int Count);