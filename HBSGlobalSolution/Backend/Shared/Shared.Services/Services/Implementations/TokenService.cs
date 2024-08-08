namespace Shared.Services.Services.Implementations;

internal sealed class TokenService(IOptions<JWTOptions> options) : ITokenService
{
    private readonly JWTOptions options = options.Value;

    public string GenerateAccessToken(int userId, BookingRole userRole, string userType)
    {
        SymmetricSecurityKey securityKey = userType == BookingUserTypeConstants.Admin
            ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.BookingAdminSecret!))
            : new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.BookingCustomerSecret!));

        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim(BookingClaimConstants.Role, userRole.ToString()),
            new Claim(BookingClaimConstants.Id, userId!.ToString()!),
        ];

        JwtSecurityToken token = userType == BookingUserTypeConstants.Admin
            ? new JwtSecurityToken(options.BookingAdminIssuer, options.BookingAdminAudience, claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(options.TokenExpirationDays)), signingCredentials: credentials)
            : new JwtSecurityToken(options.BookingCustomerIssuer, options.BookingCustomerAudience, claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(options.TokenExpirationDays)), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}