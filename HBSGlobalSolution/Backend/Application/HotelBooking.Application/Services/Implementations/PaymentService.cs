namespace HotelBooking.Application.Services.Implementations;

public sealed class PaymentService(
    IUnitOfWork unitOfWork, 
    IMonoPayService monoPayService) : IPaymentService
{
    public async Task<ApplicationResult<InvoiceMonoGetDTO>> CreateMonoPayment(PaymentCreateDTO paymentCreateDTO)
    {
        RoomInternalGetDTO? room = await unitOfWork.RoomRepository
            .GetItemAsync(RoomExpressions.MapToRoomInternalDTO, x => x.Id == paymentCreateDTO.RoomId);

        if (room is null)
            return ApplicationResult<InvoiceMonoGetDTO>.Fail(BookingStatusCodes.NotFound, "Room is not found!");

        InvoiceMonoCreateDTO invoiceMonoCreateDTO = new((int)(room.Amount * 100));

        return await monoPayService.CreateInvoice(invoiceMonoCreateDTO);
    }
}