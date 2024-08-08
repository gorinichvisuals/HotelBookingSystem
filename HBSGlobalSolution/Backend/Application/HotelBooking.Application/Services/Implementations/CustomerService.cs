namespace HotelBooking.Application.Services.Implementations;

public sealed class CustomerService(
    IUnitOfWork unitOfWork, 
    ILinkBuilderService linkBuilderService) : ICustomerService
{
    public async Task<ApplicationResult<CustomerDTO>> GetCustomer(int customerId, CancellationToken cancellationToken)
    {
        CustomerDTO? customer = await unitOfWork.CustomerRepository.GetItemAsync(
            CustomerExpressions.MapToCustomerDTO, c => c.Id == customerId, cancellationToken);

        if (customer is null)
            return ApplicationResult<CustomerDTO>.Fail(BookingStatusCodes.NotFound, "Customer is not found.");

        foreach (var booking in customer.Bookings)
            booking.Room.Photos = booking.Room.Photos!.Select(linkBuilderService.CreatePhotoLink).ToList()!;

        return ApplicationResult<CustomerDTO>.Success(BookingStatusCodes.Ok, customer);
    }

    public async Task<ApplicationResult<CustomerDTO>> UpdateCustomer(CustomerUpdateDTO updateDTO, int customerId)
    {
        bool customerExists = await unitOfWork.CustomerRepository.AnyAsync(c => c.Id == customerId);

        if (!customerExists)
            return ApplicationResult<CustomerDTO>.Fail(BookingStatusCodes.NotFound, "Customer is not found.");

        await unitOfWork.CustomerRepository
            .UpdateCustomer(c => c.Id == customerId, updateDTO.Name, updateDTO.Email, updateDTO.Phone);

        CustomerDTO? customer = await unitOfWork.CustomerRepository.GetItemAsync(
            CustomerExpressions.MapToCustomerDTO, c => c.Id == customerId);

        foreach (var booking in customer!.Bookings)
            booking.Room.Photos = booking.Room.Photos!.Select(linkBuilderService.CreatePhotoLink).ToList()!;

        return ApplicationResult<CustomerDTO>.Success(BookingStatusCodes.Ok, customer);
    }

    public async Task<ApplicationResult> ChangePassword(int customerId, string oldPassword, string newPassword)
    {
        string? existingPassword = await unitOfWork.CustomerRepository.GetItemAsync(c => c.Password, c => c.Id == customerId);

        if (existingPassword is null)
            return ApplicationResult<CustomerDTO>.Fail(BookingStatusCodes.NotFound, "Customer is not found.");

        if (!BCrypt.Net.BCrypt.Verify(oldPassword, existingPassword))
            return ApplicationResult<AuthTokenDTO>.Fail(BookingStatusCodes.BadRequest, "Wrong password.");

        string? newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, workFactor: BookingWorkFactor.CustomerWorkFactor);

        await unitOfWork.CustomerRepository.ChangePassword(c => c.Id == customerId, newHashedPassword);

        return ApplicationResult.Success(BookingStatusCodes.Ok);
    }
}