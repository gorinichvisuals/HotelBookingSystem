namespace HotelBooking.Application.Services.Abstractions;

public interface IPaymentService
{
    Task<ApplicationResult<InvoiceMonoGetDTO>> CreateMonoPayment(PaymentCreateDTO paymentCreateDTO);
}