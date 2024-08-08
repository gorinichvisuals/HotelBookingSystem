namespace HotelBooking.Application.Services.Abstractions;

public interface IAuthService
{
    Task<ApplicationResult<AuthTokenDTO>> CreateCustomer(CustomerCreateDTO createDTO);
    Task<ApplicationResult<AuthTokenDTO>> Login(AuthLoginDTO loginDTO);
}