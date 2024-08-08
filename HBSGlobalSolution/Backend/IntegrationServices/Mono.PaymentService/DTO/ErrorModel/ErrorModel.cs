global using System.Text.Json.Serialization;

namespace Mono.PaymentService.DTO.ErrorModel;

internal sealed class ErrorModel
{
    [JsonPropertyName("errCode")]
    public string? ErrCode { get; set; }

    [JsonPropertyName("errText")]
    public string? ErrText { get; set; }
}