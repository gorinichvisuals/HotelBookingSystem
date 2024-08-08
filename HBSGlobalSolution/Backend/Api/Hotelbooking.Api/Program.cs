var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.DALExtentions();
builder.Services.InfrastructureExtentions();
builder.Services.ApplicationServicesExtentions();
builder.Services.SharedServicesExtentions();
builder.Services.ConfigureRedisAndCloudFront(builder.Configuration, out var connectionMultiplexer, builder.Environment.EnvironmentName);
builder.Services.JWTTokenExtensions(builder.Configuration, BookingUserTypeConstants.Customer, builder.Environment.EnvironmentName);
builder.Services.AddBookingRateLimiter(builder.Configuration, connectionMultiplexer, builder.Environment.EnvironmentName);
builder.Services.MonoPayServicesExtentions(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.AddScoped<ISessionProvider, SessionProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Customer Api v1");
    });
}

app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod());

app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();