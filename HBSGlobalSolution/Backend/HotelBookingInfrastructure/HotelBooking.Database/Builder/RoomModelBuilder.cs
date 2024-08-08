namespace HotelBooking.Database.Builder;

internal static class RoomModelBuilder
{
    public static void ConfigureRoomModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
            .Property(x => x.RoomType)
            .HasConversion<string>();

        modelBuilder.Entity<Room>()
            .Property(x => x.RoomClass)
            .HasConversion<string>();

        modelBuilder.Entity<Room>()
            .Property(x => x.Amount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Room>()
            .Property(x => x.IsAvailable)
            .HasConversion<bool>();

        modelBuilder.Entity<Room>()
            .HasMany(x => x.Photos)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}