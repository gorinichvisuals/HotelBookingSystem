namespace Mono.PaymentService.DTO.Requests;

public sealed class InvoiceMerchantPaymInfoModel
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("destination")]
    public string? Destination { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("basketOrder")]
    public ICollection<InvoiceBasketOrderModel>? BasketOrder { get; set; }
}