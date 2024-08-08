namespace HotelBooking.Database.Config;

public static class DbConnectionManager
{
    public static string HotelBookingConnectionString { get; set; }

    static DbConnectionManager()
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        string? binDirectory = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
            ? AppDomain.CurrentDomain.BaseDirectory
            : AppDomain.CurrentDomain.RelativeSearchPath;

        IConfigurationRoot root = new ConfigurationBuilder()
            .SetBasePath(binDirectory)
            .AddJsonFile($"dal.{environment}.json", optional: false, reloadOnChange: true)
            .Build();

        HotelBookingConnectionString = root.GetSection(nameof(DataOptions))!.Get<DataOptions>()!.HotelBookingConnectionString!;
    }   
}