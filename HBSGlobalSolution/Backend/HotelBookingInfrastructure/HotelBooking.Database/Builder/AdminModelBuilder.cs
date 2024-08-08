namespace HotelBooking.Database.Builder;

internal static class AdminModelBuilder
{
    public static void ConfigureAdminModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Admin>()
            .HasIndex(x => x.Phone)
            .IsUnique();

        modelBuilder.Entity<Admin>()
            .Property(x => x.Role)
            .HasConversion<string>();
    }
}