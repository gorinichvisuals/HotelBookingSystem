namespace HotelBooking.Application.DTO.Room;

public sealed class RoomFilterDTO
{
    public int SkipItems { get; set; } = 0;
    public int TakeItems { get; set; } = 10;
    public bool Ascending { get; set; } = true;
    public string? RoomType { get; set; }
    public string? RoomClass { get; set; }
    public int? Floor {  get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}