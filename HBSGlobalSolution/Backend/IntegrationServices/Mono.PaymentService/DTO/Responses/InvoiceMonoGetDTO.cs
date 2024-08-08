namespace Mono.PaymentService.DTO.Responses;

public sealed class InvoiceMonoGetDTO
{
    [JsonPropertyName("invoiceId")]
    public string? InvoiceId { get; set; }

    [JsonPropertyName("pageUrl")]
    public string? PageURL { get; set; }
}