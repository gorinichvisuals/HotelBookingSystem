namespace Hotelbooking.Api.Controllers;

[Route("api/customers")]
[ApiController]
[Authorize(Roles = nameof(BookingRole.Customer))]
public class CustomerController(
    ICustomerService customerService,
    ISessionProvider sessionProvider,
    ILogger<CustomerController> logger) : ControllerBase
{
    [HttpGet("personal-info")]
    [SwaggerResponse(BookingStatusCodes.Ok, type: typeof(CustomerDTO))]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> GetCustomerPersonalInfo(CancellationToken cancellationToken)
    {
        try
        {
            int customerId = sessionProvider.GetId();
            var result = await customerService.GetCustomer(customerId, cancellationToken);

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

    [HttpPut("update")]
    [SwaggerResponse(BookingStatusCodes.Ok, type: typeof(CustomerDTO))]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateDTO updateDTO)
    {
        try
        {
            int customerId = sessionProvider.GetId();
            var result = await customerService.UpdateCustomer(updateDTO, customerId);

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

    [HttpPut("change-password")]
    [SwaggerResponse(BookingStatusCodes.Ok)]
    [SwaggerResponse(BookingStatusCodes.Unauthorized, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.Forbidden, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.NotFound, type: typeof(string))]
    [SwaggerResponse(BookingStatusCodes.InternalServerError, type: typeof(string))]
    public async Task<IActionResult> ChangePassword([FromBody] CustomerChangePasswordDTO changePasswordDTO)
    {
        try
        {
            int customerId = sessionProvider.GetId();
            var result = await customerService.ChangePassword(customerId, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

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