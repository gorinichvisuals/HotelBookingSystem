namespace Shared.Services.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(int userId, BookingRole userRole, string userType);
}