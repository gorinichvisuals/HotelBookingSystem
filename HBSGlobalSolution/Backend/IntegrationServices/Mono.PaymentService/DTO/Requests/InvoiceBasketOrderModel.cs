namespace Mono.PaymentService.DTO.Requests;

public sealed class InvoiceBasketOrderModel
{
    [JsonRequired]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonRequired]
    [JsonPropertyName("qty")]
    public long Qty { get; set; }

    [JsonRequired]
    [JsonPropertyName("sum")]
    public long Sum { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("unit")]
    public string? Unit { get; set; }

    [JsonRequired]
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("barcode")]
    public string? BarCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("header")]
    public string? Header { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("footer")]
    public string? Footer { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("tax")]
    public string[]? Tax { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("uktzed")]
    public string? Uktzed { get; set; }
}