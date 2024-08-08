namespace HotelBooking.Application.DTO.Booking;

public sealed record BookingDTO(
    int Id,
    string Amount,
    bool ConfirmedByAdmin,
    bool IsPaid,
    bool IsCancelled,
    string PaymentType,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    RoomDTO Room,
    PaymentDTO? Payment);