namespace HotelBooking.Application.MappingExpressions;

public static class RoomExpressions
{
    public static readonly Expression<Func<Room, RoomDTO>> MapToRoomDTO = r => new RoomDTO(
        r.Id,
        r.RoomNumber,
        r.RoomType.ToString(),
        r.RoomClass.ToString(),
        r.Description,
        $"{r.Amount}" + ' ' + "UAH",
        r.IsAvailable)
    {
        Photos = r.Photos!.Select(p => p.PhotoId).ToList()!
    };

    public static readonly Expression<Func<Room, RoomInternalGetDTO>> MapToRoomInternalDTO = r => new RoomInternalGetDTO(r.Amount);
}