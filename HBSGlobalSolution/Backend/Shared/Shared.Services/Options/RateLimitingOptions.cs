namespace Shared.Services.Options;

public sealed class RateLimitingOptions
{
    public List<RateLimitRule>? RateLimitRules { get; set; }
}