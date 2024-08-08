namespace Shared.Services.Config;

public static class ConfigurationManager
{
    public static void SharedServicesExtentions(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILinkBuilderService, LinkBuilderService>();
    }

    public static void ConfigureRedisAndCloudFront(this IServiceCollection services, IConfigurationBuilder configurationBuilder, 
        out IConnectionMultiplexer connectionMultiplexer, string environment)
    {
        string? binDirectory = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
            ? AppDomain.CurrentDomain.BaseDirectory
            : AppDomain.CurrentDomain.RelativeSearchPath;

        IConfigurationRoot root = configurationBuilder
            .SetBasePath(binDirectory)
            .AddJsonFile($"options.{environment}.json", false, true)
            .Build();

        var clusterEndpoint = string.Format(
            root.GetSection(nameof(RedisOptions)).Get<RedisOptions>()!.ConnectionString!,
            root.GetSection(nameof(RedisOptions)).Get<RedisOptions>()!.AuthKey);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = clusterEndpoint;
            options.InstanceName = root.GetSection(nameof(RedisOptions)).Get<RedisOptions>()!.Name;
        });

        var connection = ConnectionMultiplexer.Connect(clusterEndpoint);
        connectionMultiplexer = connection;

        services.Configure<CloudFrontOptions>(root.GetSection(nameof(CloudFrontOptions)));
    }

    public static void JWTTokenExtensions(this IServiceCollection services, 
        IConfigurationBuilder configurationBuilder, string userType, string environment)
    {
        string? binDirectory = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
            ? AppDomain.CurrentDomain.BaseDirectory
            : AppDomain.CurrentDomain.RelativeSearchPath;

        IConfigurationRoot root = configurationBuilder
            .SetBasePath(binDirectory)
            .AddJsonFile($"options.{environment}.json", false, true)
            .Build();

        JWTOptions? jwt = root.GetSection(nameof(JWTOptions)).Get<JWTOptions>();

        services.Configure<JWTOptions>(options =>
        {
            options.BookingCustomerIssuer = jwt!.BookingCustomerIssuer;
            options.BookingCustomerAudience = jwt.BookingCustomerAudience;
            options.BookingCustomerSecret = jwt.BookingCustomerSecret;

            options.BookingAdminIssuer = jwt.BookingAdminIssuer;
            options.BookingAdminAudience = jwt.BookingAdminAudience;
            options.BookingAdminSecret = jwt.BookingAdminSecret;

            options.TokenExpirationDays = jwt.TokenExpirationDays;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer =
                    userType == BookingUserTypeConstants.Admin
                        ? jwt!.BookingAdminIssuer
                        : jwt!.BookingCustomerIssuer,
                ValidAudience =
                    userType == BookingUserTypeConstants.Admin
                        ? jwt.BookingAdminAudience
                        : jwt.BookingCustomerAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    userType == BookingUserTypeConstants.Admin
                        ? jwt.BookingAdminSecret!
                        : jwt.BookingCustomerSecret!))
            };
        });

        services.AddAuthorization();
    }

    public static void AddBookingRateLimiter(this IServiceCollection services,
        IConfiguration configuration, IConnectionMultiplexer connectionMultiplexer, string environment)
    {
        string envPrefix = string.Concat(environment, "_");
        RateLimitingOptions? rateLimitingOptions = configuration.GetSection(nameof(RateLimitingOptions)).Get<RateLimitingOptions>();

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = BookingStatusCodes.TooManyRequests;

            options.OnRejected = async (context, cancellationToken) =>
            {
                string messageToClient = string.Empty;

                if (context.Lease.TryGetMetadata(RateLimitMetadataName.Limit, out var limit))
                {
                    string ruleName = (string)context.HttpContext.Items["RuleName"]!;

                    RateLimitRule? rateLimitRule =
                        rateLimitingOptions!.RateLimitRules!.FirstOrDefault(_ => _.EndpointName == ruleName);

                    if (rateLimitRule is not null)
                    {
                        int seconds = rateLimitRule.PeriodSeconds;

                        messageToClient = string.Format(RateLimitingConstants.ErrorMessageTemplate, limit, seconds);
                    }
                }

                messageToClient = string.IsNullOrEmpty(messageToClient)
                    ? RateLimitingConstants.DefaultErrorMessage
                    : messageToClient;

                if (context.Lease.TryGetMetadata(RateLimitMetadataName.RetryAfter, out var retryAfter))
                {
                    messageToClient += $" Please retry after {retryAfter} seconds.";
                }

                await context.HttpContext.Response.WriteAsync(messageToClient, cancellationToken: cancellationToken);
            };

            foreach (RateLimitRule rule in rateLimitingOptions!.RateLimitRules!)
            {
                options.AddPolicy(rule.EndpointName!, httpContext =>
                {
                    httpContext.Items["RuleName"] = rule.EndpointName;
                    RouteValueDictionary routeValues = httpContext.Request.RouteValues;
                    int permitLimit = rule.PermitLimit;
                    string ipAddress = httpContext.Connection.RemoteIpAddress!.ToString() ?? string.Empty;

                    return GetRateLimitPartition(permitLimit, ipAddress);
                });

                RateLimitPartition<string> GetRateLimitPartition(int permitLimit, string ipAddress)
                {
                    return RedisRateLimitPartition.GetFixedWindowRateLimiter(envPrefix + rule.EndpointName + "_" + ipAddress, _ =>
                        new RedisFixedWindowRateLimiterOptions()
                        {
                            PermitLimit = permitLimit,
                            Window = TimeSpan.FromSeconds(rule.PeriodSeconds),
                            ConnectionMultiplexerFactory = () => connectionMultiplexer,
                        });
                }
            }
        });
    }

    private async static Task TaskAsync(ForbiddenContext forbiddenContext)
    {

    }
}