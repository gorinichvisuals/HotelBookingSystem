namespace HotelBooking.Api.Controllers;

[Route("api/payments")]
[ApiController]
[Authorize(Roles = nameof(BookingRole.Customer))]
public class PaymentController(
    IPaymentService paymentService, 
    ILogger<PaymentController> logger) : ControllerBase
{
    [HttpPost("create-invoice")]
    [SwaggerResponse(BookingStatusCodes.Ok, type: typeof(InvoiceMonoGetDTO))]
    [SwaggerResponse(BookingStatusCodes.BadRequest, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.TooManyRequests, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> CreateMonoPayment(PaymentCreateDTO paymentCreateDTO)
    {
        try
        {
            var result = await paymentService.CreateMonoPayment(paymentCreateDTO);

            return result.IsSucceed
                ? StatusCode(result.StatusCode, result.Result)
                : StatusCode(result.StatusCode, new { result.ErrorMessage });
        }
        catch (Exception exception)
        {
            logger.LogError("Log error message: {ex}", exception.GetBaseException().Message);

            return StatusCode(BookingStatusCodes.InternalServerError, new { exception.Message });
        }
    }
}