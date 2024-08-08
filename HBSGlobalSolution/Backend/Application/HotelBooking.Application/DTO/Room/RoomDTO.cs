namespace HotelBooking.Application.DTO.Room;

public sealed record RoomDTO(
    int Id,
    int RoomNumber,
    string RoomType,
    string RoomClass,
    string? Description,
    string Amount,
    bool IsAvailable)
{ 
    public ICollection<string>? Photos { get; set; }
};