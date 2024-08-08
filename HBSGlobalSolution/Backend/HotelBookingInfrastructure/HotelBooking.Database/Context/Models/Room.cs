namespace HotelBooking.Database.Context.Models;

public sealed class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomNumber { get; set; }

    [Required]
    [StringLength(16)]
    public RoomType RoomType { get; set; }

    [Required]
    [StringLength(16)]
    public RoomClass RoomClass { get; set; }

    [Required]
    [StringLength(1024)]
    public string? Description { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    [Required]
    public int Floor {  get; set; }

    public ICollection<Booking>? Bookings { get; set; }
    public ICollection<RoomPhoto>? Photos { get; set; }
}