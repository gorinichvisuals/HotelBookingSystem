namespace HotelBooking.Application.DTO.Auth;

public sealed record AuthInternalDTO(int Id, BookingRole Role, string Password);