namespace Shared.Services.Options;

public sealed class RedisOptions
{
    public string? AuthKey { get; set; }
    public string? ConnectionString { get; set; }
    public string? Name { get; set; }
}