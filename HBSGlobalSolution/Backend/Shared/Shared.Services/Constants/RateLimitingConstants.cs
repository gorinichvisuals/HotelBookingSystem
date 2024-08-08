namespace Shared.Services.Constants;

public static class RateLimitingConstants
{
    public const string CustomerAuth = "CustomerAuth";
    public const string CustomerResetPassword = "CustomerResetPassword";
    public const string GetRooms = "GetRooms";

    public const string DefaultErrorMessage =
    "Too Many Requests: You have exceeded the rate limit for requests on this endpoint. Please wait before trying again.";
    public const string ErrorMessageTemplate = "Maximum of {0} request every {1} seconds allowed.";
}