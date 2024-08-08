namespace HotelBooking.Api.Services;

public sealed class SessionProvider(IHttpContextAccessor contextAccessor) : ISessionProvider
{
    public int GetId()
    {
        return int.Parse(contextAccessor.HttpContext!.User.FindFirstValue(BookingClaimConstants.Id)!);
    }
}