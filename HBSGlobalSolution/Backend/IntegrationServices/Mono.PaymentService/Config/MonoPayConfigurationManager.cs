namespace Mono.PaymentService.Config;

public static class MonoPayConfigurationManager
{
    public static void MonoPayServicesExtentions(this IServiceCollection services, IConfigurationBuilder configurationBuilder, string environment)
    {
        services.AddScoped<IMonoPayService, MonoPayService>();

        string? binDirectory = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
            ? AppDomain.CurrentDomain.BaseDirectory
            : AppDomain.CurrentDomain.RelativeSearchPath;

        IConfigurationRoot root = configurationBuilder
            .SetBasePath(binDirectory)
            .AddJsonFile($"options.mono.{environment}.json", false, true)
            .Build();

        services.Configure<MonoBankOptions>(root.GetSection(nameof(MonoBankOptions)));
        services.Configure<BookingUrlOptions>(root.GetSection(nameof(BookingUrlOptions)));

        services.AddHttpClient(HttpClientConstants.MonoBank, httpClient =>
        {
            httpClient.BaseAddress = new Uri(root.GetSection(nameof(MonoBankOptions)).Get<MonoBankOptions>()!.BaseUrl!);
            httpClient.DefaultRequestHeaders.Add("X-Token", root.GetSection(nameof(MonoBankOptions)).Get<MonoBankOptions>()!.XToken);
        });
    }
}