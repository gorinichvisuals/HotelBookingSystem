namespace HotelBooking.Application.Services.Abstractions;

public interface ICustomerService
{
    Task<ApplicationResult<CustomerDTO>> GetCustomer(int customerId, CancellationToken cancellationToken);
    Task<ApplicationResult<CustomerDTO>> UpdateCustomer(CustomerUpdateDTO updateDTO, int customerId);
    Task<ApplicationResult> ChangePassword(int customerId, string oldPassword, string newPassword);
}