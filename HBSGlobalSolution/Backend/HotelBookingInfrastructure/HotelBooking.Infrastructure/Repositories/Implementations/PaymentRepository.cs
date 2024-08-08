namespace HotelBooking.Infrastructure.Repositories.Implementations;

internal sealed class PaymentRepository(BookingContext context) : IPaymentRepository
{
    public BookingContext Context { get; set; } = context;
}