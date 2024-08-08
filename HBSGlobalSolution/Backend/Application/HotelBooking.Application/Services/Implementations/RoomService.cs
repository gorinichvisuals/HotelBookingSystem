namespace HotelBooking.Application.Services.Implementations;

public sealed class RoomService(
    IUnitOfWork unitOfWork,
    ILinkBuilderService linkBuilderService) : IRoomService
{
    public async Task<RoomsDTO> GetRooms(RoomFilterDTO filterDTO, CancellationToken cancellationToken)
    {
        (ICollection<RoomDTO> rooms, int count) = await unitOfWork.RoomRepository.GetFilteredRooms(
            RoomExpressions.MapToRoomDTO, 
            filterDTO.SkipItems, 
            filterDTO.TakeItems,
            filterDTO.Ascending,
            filterDTO.RoomType!,
            filterDTO.RoomClass!,
            filterDTO.Floor,
            filterDTO.CheckInDate, 
            filterDTO.CheckOutDate,
            cancellationToken);

        foreach(var room in rooms)
            room.Photos = room.Photos!.Select(linkBuilderService.CreatePhotoLink).ToList()!;

        return new RoomsDTO(rooms, count);
    }
}