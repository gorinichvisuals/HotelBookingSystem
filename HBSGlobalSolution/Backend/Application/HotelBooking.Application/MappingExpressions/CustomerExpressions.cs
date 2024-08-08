namespace HotelBooking.Application.MappingExpressions;

public static class CustomerExpressions
{
    public static readonly Expression<Func<Customer, CustomerDTO>> MapToCustomerDTO = c => new CustomerDTO(
        c.Name!,
        c.Email!,
        c.Phone!,
        DateTime.SpecifyKind(c.RegisterAt, DateTimeKind.Utc),
        c.Bookings!.Select(b => new BookingDTO(
            b.Id,
            $"{b.Amount}" + ' ' + "UAH",
            b.ConfirmedByAdmin,
            b.IsPaid,
            b.IsCancelled,
            b.PaymentType.ToString(),
            DateTime.SpecifyKind(b.CheckInDate, DateTimeKind.Utc),
            DateTime.SpecifyKind(b.CheckOutDate, DateTimeKind.Utc),
            new RoomDTO(
                b.Room!.Id,
                b.Room.RoomNumber,
                b.Room.RoomType.ToString(),
                b.Room.RoomClass.ToString(),
                b.Room.Description,
                $"{b.Amount}" + ' ' + "UAH",
                b.Room.IsAvailable)
            {
                Photos = b.Room.Photos!.Select(p => p.PhotoId).ToList()!
            },
            b.Payment != null 
                ? new PaymentDTO(
                    b.Payment!.Id,
                    b.Payment.PaymentId!,
                    $"{b.Amount}" + ' ' + "UAH",
                    DateTime.SpecifyKind(b.Payment.PaymentDate, DateTimeKind.Utc),
                    b.Payment.PaymentStatus.ToString())
            : null))
        .ToList());
}