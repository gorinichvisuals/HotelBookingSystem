namespace HotelBooking.Database.Context.Models;

public sealed class RoomPhoto
{
    [Key]
    [Required]
    [StringLength(16)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string? PhotoId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public Room? Room { get; set; }
}