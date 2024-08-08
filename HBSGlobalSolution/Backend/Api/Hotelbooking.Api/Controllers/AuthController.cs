namespace Hotelbooking.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    IAuthService authService, 
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("create")]
    [SwaggerResponse(BookingStatusCodes.Created, type: typeof(AuthTokenDTO))]
    [SwaggerResponse(BookingStatusCodes.BadRequest, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.TooManyRequests, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    [EnableRateLimiting(RateLimitingConstants.CustomerAuth)]
    public async Task<IActionResult> CreateCustomer(CustomerCreateDTO customerCreateDTO)
    {
        try
        {
            var result = await authService.CreateCustomer(customerCreateDTO);

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

    [HttpPost("login")]
    [SwaggerResponse(BookingStatusCodes.Ok, type: typeof(AuthTokenDTO))]
    [SwaggerResponse(BookingStatusCodes.BadRequest, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.TooManyRequests, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    [EnableRateLimiting(RateLimitingConstants.CustomerAuth)]
    public async Task<IActionResult> Login(AuthLoginDTO loginDTO)
    {
        try
        {
            var result = await authService.Login(loginDTO);

            return result.IsSucceed
                ? StatusCode(result.StatusCode, result.Result)
                : StatusCode(result.StatusCode, result.ErrorMessage);
        }
        catch (Exception exception)
        {
            logger.LogError("Log error message: {ex}", exception.GetBaseException().Message);

            return StatusCode(BookingStatusCodes.InternalServerError, new { exception.Message });
        }
    }
}