namespace HotelBooking.Infrastructure.Repositories.UOW;

public interface IUnitOfWork
{
    IAdminRepository AdminRepository { get; }
    IBookingRepository BookingRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IRoomPhotoRepository RoomPhotoRepository { get; }
    IRoomRepository RoomRepository { get; }

    Task SaveChanges();
}