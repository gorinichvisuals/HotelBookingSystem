namespace HotelBooking.Application.DTO.Payment;

public sealed record PaymentDTO(
    int Id,
    string PaymentId,
    string Amount,
    DateTime PaymentDate,
    string PaymentStatus);