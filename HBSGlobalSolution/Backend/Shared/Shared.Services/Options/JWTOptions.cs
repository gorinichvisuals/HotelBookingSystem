namespace Shared.Services.Options;

public sealed class JWTOptions
{
    public string? BookingCustomerSecret { get; set; }
    public string? BookingCustomerIssuer { get; set; }
    public string? BookingCustomerAudience { get; set; }
    public string? BookingAdminSecret { get; set; }
    public string? BookingAdminIssuer { get; set; }
    public string? BookingAdminAudience { get; set; }
    public int TokenExpirationDays { get; set; }
}