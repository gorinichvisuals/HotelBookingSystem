namespace HotelBooking.Application.DTO.Customer;

public sealed record CustomerCreateDTO(
    [field: Required, EmailAddress, StringLength(128)] string Name,
    [field: Required, EmailAddress, StringLength(128)] string Email,
    [field: Required, EmailAddress, StringLength(16)] string Phone,
    [field: Required] string Password);