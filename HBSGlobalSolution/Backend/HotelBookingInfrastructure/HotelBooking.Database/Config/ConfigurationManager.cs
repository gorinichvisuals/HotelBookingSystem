namespace HotelBooking.Database.Config;

public static class ConfigurationManager
{
    public static void DALExtentions(this IServiceCollection services)
    {
        services.AddDbContext<BookingContext>();
    }
}