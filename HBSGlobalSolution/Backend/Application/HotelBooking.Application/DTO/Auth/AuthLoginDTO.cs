namespace HotelBooking.Application.DTO.Auth;

public sealed record AuthLoginDTO(
    [field: Required, EmailAddress, StringLength(128)] string Email,
    [field: Required] string Password);