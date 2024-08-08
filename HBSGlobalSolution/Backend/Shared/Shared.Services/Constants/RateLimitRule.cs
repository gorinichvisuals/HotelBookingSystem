namespace Shared.Services.Constants;

public sealed class RateLimitRule
{
    public string? EndpointName { get; set; }
    public int PermitLimit { get; set; }
    public int PeriodSeconds { get; set; }
}