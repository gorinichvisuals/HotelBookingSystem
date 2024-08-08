namespace HotelBooking.Infrastructure.Config;

public static class ConfigurationManager
{
    public static void InfrastructureExtentions(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IRoomPhotoRepository, RoomPhotoRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
    }
}