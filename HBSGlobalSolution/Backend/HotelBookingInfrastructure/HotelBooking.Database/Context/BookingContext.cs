namespace HotelBooking.Database.Context;

public sealed class BookingContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomPhoto> RoomPhotos { get; set; }

    public BookingContext()
    {
        
    }

    public BookingContext(DbContextOptions<BookingContext> options) 
        : base(options) 
    { 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    #if DEBUG
        var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        optionsBuilder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
    #endif
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(DbConnectionManager.HotelBookingConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AdminModelBuilder.ConfigureAdminModel(modelBuilder);
        BookingModelBuilder.ConfigureBookingModel(modelBuilder);
        CustomerModelBuilder.ConfigureCustomerModel(modelBuilder);
        PaymentModelBuilder.ConfigurePaymentModel(modelBuilder);
        RoomModelBuilder.ConfigureRoomModel(modelBuilder);

        base.OnModelCreating(modelBuilder); 
    }
}