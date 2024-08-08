namespace HotelBooking.Application.Config;

public static class ConfigurationManager
{
    public static void ApplicationServicesExtentions(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IRoomService, RoomService>();
    }
}