namespace HotelBooking.Application.DTO.Booking;

public sealed record BookingCreateDTO(
    int RoomId,
    PaymentType PaymentType,
    DateTime CheckInDate,
    DateTime CheckOutDate);