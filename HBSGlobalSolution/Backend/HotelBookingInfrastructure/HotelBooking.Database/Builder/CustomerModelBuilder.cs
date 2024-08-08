namespace HotelBooking.Database.Builder;

internal static class CustomerModelBuilder
{
    public static void ConfigureCustomerModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.Phone)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .Property(x => x.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Customer>()
            .Property(x => x.RegisterAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}