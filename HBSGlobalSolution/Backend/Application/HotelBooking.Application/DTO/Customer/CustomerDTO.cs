namespace HotelBooking.Application.DTO.Customer;

public sealed record CustomerDTO(
    string Name,
    string Email,
    string Phone,
    DateTime RegisterAt,
    ICollection<BookingDTO> Bookings);