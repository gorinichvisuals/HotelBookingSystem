namespace Mono.PaymentService.Services.Abstractions;

public interface IMonoPayService
{
    Task<ApplicationResult<InvoiceMonoGetDTO>> CreateInvoice(InvoiceMonoCreateDTO createDTO);
}