namespace HotelBooking.Database.Context.Models;

public sealed class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int AdminId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public bool ConfirmedByAdmin { get; set; }

    [Required]
    public bool IsPaid { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Required]
    public PaymentType PaymentType { get; set; }

    [Required]
    public bool IsCancelled { get; set; }

    [Required]
    public Room? Room { get; set; }

    [Required]
    public Customer? Customer { get; set; }

    [Required]
    public Admin? Admin { get; set; }

    [Required]
    public Payment? Payment { get; set; }
}