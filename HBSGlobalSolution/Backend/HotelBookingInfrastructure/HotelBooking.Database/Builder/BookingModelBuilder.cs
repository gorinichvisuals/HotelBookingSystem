namespace HotelBooking.Database.Builder;

internal static class BookingModelBuilder
{
    public static void ConfigureBookingModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .Property(x => x.Amount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Booking>()
            .Property(x => x.PaymentType)
            .HasConversion<string>();

        modelBuilder.Entity<Booking>()
            .HasOne(x => x.Room)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Booking>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Booking>()
            .HasOne(x => x.Admin)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.AdminId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Booking>()
            .HasOne(x => x.Payment)
            .WithOne(x => x.Booking)
            .OnDelete(DeleteBehavior.NoAction);
    }
}