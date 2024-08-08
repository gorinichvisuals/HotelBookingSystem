namespace HotelBooking.Api.Controllers;

[Route("api/bookings")]
[ApiController]
[Authorize(Roles = nameof(BookingRole.Customer))]
public class BookingController(
    IBookingService bookingService, 
    ISessionProvider sessionProvider,
    ILogger<BookingController> logger) : ControllerBase
{
    [HttpPost("create")]
    [SwaggerResponse(BookingStatusCodes.Created, type: typeof(BookingDTO))]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> CreateBooking(BookingCreateDTO bookingCreateDTO)
    {
        try
        {
            var customerId = sessionProvider.GetId();
            var result = await bookingService.CreateBooking(bookingCreateDTO, customerId);

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

    [HttpPut("update/{bookingId}")]
    [SwaggerResponse(BookingStatusCodes.Ok, type: typeof(BookingDTO))]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> UpdateBookingBooking(BookingUpdateDTO bookingUpdateDTO, int bookingId)
    {
        try
        {
            var result = await bookingService.UpdateBooking(bookingUpdateDTO, bookingId);

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

    [HttpPut("cancel/{bookingId}")]
    [SwaggerResponse(BookingStatusCodes.Ok)]
    [SwaggerResponse(BookingStatusCodes.BadRequest, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> CancelBooking(int bookingId)
    {
        try
        {
            var result = await bookingService.CancelBooking(bookingId);

            return result.IsSucceed
                ? StatusCode(result.StatusCode)
                : StatusCode(result.StatusCode, new { result.ErrorMessage });
        }
        catch (Exception exception)
        {
            logger.LogError("Log error message: {ex}", exception.GetBaseException().Message);

            return StatusCode(BookingStatusCodes.InternalServerError, new { exception.Message });
        }
    }
}