namespace HotelBooking.Database.Context.Models;

public sealed class Payment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? PaymentId { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    [Required]
    public PaymentStatus PaymentStatus { get; set; }

    [Required]
    public Booking? Booking { get; set; }
}