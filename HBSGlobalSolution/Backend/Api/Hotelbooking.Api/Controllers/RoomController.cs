namespace HotelBooking.Api.Controllers;

[Route("api/rooms")]
[ApiController]
public class RoomController(
    IRoomService roomService, 
    ILogger<RoomController> logger) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(BookingStatusCodes.Ok, type: typeof(RoomsDTO))]
    [SwaggerResponse(BookingStatusCodes.TooManyRequests, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    [EnableRateLimiting(RateLimitingConstants.GetRooms)]
    public async Task<IActionResult> GetRooms([FromQuery] RoomFilterDTO filterDTO, CancellationToken cancellationToken)
    {
        try
        {
            var result = await roomService.GetRooms(filterDTO, cancellationToken);

            return StatusCode(BookingStatusCodes.Ok, result);

        }
        catch (Exception exception)
        {
            logger.LogError("Log error message: {ex}", exception.GetBaseException().Message);

            return StatusCode(BookingStatusCodes.InternalServerError, new { exception.Message });
        }
    }
}