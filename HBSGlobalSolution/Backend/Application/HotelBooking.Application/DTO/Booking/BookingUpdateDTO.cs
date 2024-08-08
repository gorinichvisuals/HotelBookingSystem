namespace HotelBooking.Application.DTO.Booking;

public sealed record BookingUpdateDTO(
    int RoomId, 
    DateTime CheckInDate,
    DateTime CheckOutDate);