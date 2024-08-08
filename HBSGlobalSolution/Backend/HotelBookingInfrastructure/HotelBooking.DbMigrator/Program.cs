using HotelBooking.Database.Config;
using HotelBooking.Database.Context;
using Microsoft.EntityFrameworkCore;

var appBuilder = new DbContextOptionsBuilder<BookingContext>()
    .UseSqlServer(DbConnectionManager.HotelBookingConnectionString);

using (var context = new BookingContext(appBuilder.Options))
{
    await context.Database.MigrateAsync();
}