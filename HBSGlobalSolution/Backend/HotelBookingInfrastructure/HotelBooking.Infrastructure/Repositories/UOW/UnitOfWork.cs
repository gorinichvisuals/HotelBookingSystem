namespace HotelBooking.Infrastructure.Repositories.UOW;

internal sealed class UnitOfWork(
    BookingContext context,
    IAdminRepository adminRepository,
    IBookingRepository bookingRepository,
    ICustomerRepository customerRepository,
    IPaymentRepository paymentRepository,
    IRoomPhotoRepository roomPhotoRepository,
    IRoomRepository roomRepository) : IUnitOfWork
{
    private readonly BookingContext context = context;

    public IAdminRepository AdminRepository => adminRepository;
    public IBookingRepository BookingRepository => bookingRepository;
    public ICustomerRepository CustomerRepository => customerRepository;
    public IPaymentRepository PaymentRepository => paymentRepository;
    public IRoomPhotoRepository RoomPhotoRepository => roomPhotoRepository;
    public IRoomRepository RoomRepository => roomRepository;

    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}