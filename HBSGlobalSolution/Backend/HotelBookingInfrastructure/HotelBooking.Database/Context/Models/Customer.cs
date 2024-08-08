namespace HotelBooking.Database.Context.Models;

public sealed class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(128)]
    public string? Name { get; set; }

    [Required]
    [StringLength(128)]
    public string? Email { get; set; }

    [Required]
    [StringLength(16)]
    public string? Phone { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public BookingRole Role { get; set; }

    [Required]
    public DateTime RegisterAt { get; set; }

    public ICollection<Booking>? Bookings { get; set; }
}