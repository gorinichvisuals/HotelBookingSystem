namespace HotelBooking.Application.Services.Implementations;

public sealed class AuthService(
    IUnitOfWork unitOfWork,
    ITokenService tokenService) : IAuthService
{
    public async Task<ApplicationResult<AuthTokenDTO>> CreateCustomer(CustomerCreateDTO createDTO)
    {
        bool sameCustomerExists = await unitOfWork.CustomerRepository
            .AnyAsync(x => x.Email == createDTO.Email || x.Phone == createDTO.Phone);

        if (sameCustomerExists)
            return ApplicationResult<AuthTokenDTO>.Fail(BookingStatusCodes.BadRequest, "User with same email or phone already exists.");

        Customer? newCustomer = new()
        {
            Name = createDTO.Name,
            Email = createDTO.Email,
            Phone = createDTO.Phone,
            Role = BookingRole.Customer,
            Password = BCrypt.Net.BCrypt.HashPassword(createDTO.Password, workFactor: BookingWorkFactor.CustomerWorkFactor),
        };

        await unitOfWork.CustomerRepository.CreateAsync(newCustomer);
        await unitOfWork.SaveChanges();

        string? jwtToken = tokenService.GenerateAccessToken(newCustomer.Id, newCustomer.Role, BookingUserTypeConstants.Customer);

        AuthTokenDTO authTokenDTO = new(jwtToken);

        return ApplicationResult<AuthTokenDTO>.Success(BookingStatusCodes.Created, authTokenDTO);
    }

    public async Task<ApplicationResult<AuthTokenDTO>> Login(AuthLoginDTO loginDTO)
    {
        AuthInternalDTO? customer = await unitOfWork.CustomerRepository
            .GetItemAsync(x => new AuthInternalDTO(x.Id, x.Role, x.Password!), x => x.Email == loginDTO.Email);

        if (customer is null)
            return ApplicationResult<AuthTokenDTO>.Fail(BookingStatusCodes.NotFound, "User is not found.");

        if(!BCrypt.Net.BCrypt.Verify(loginDTO.Password, customer.Password))
            return ApplicationResult<AuthTokenDTO>.Fail(BookingStatusCodes.BadRequest, "Wrong password.");

        string? jwtToken = tokenService.GenerateAccessToken(customer.Id, customer.Role, BookingUserTypeConstants.Customer);

        AuthTokenDTO authTokenDTO = new(jwtToken);

        return ApplicationResult<AuthTokenDTO>.Success(BookingStatusCodes.Ok, authTokenDTO);
    }
}