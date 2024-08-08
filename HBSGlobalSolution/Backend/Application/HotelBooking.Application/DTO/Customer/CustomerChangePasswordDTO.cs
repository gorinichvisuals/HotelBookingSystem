namespace HotelBooking.Application.DTO.Customer;

public sealed record CustomerChangePasswordDTO(
    [field: Required] string OldPassword, 
    [field: Required] string NewPassword);