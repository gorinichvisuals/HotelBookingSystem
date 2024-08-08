namespace HotelBooking.Database.Builder;

internal static class PaymentModelBuilder
{
    public static void ConfigurePaymentModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>()
            .Property(x => x.PaymentDate)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Payment>()
            .Property(x => x.PaymentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Payment>()
            .Property(x => x.Amount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Payment>()
            .HasIndex(x => x.PaymentId)
            .IsUnique();

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Booking)
            .WithOne(x => x.Payment)
            .OnDelete(DeleteBehavior.NoAction);
    }
}