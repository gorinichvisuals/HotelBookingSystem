namespace HotelBooking.Infrastructure.Repositories.Abstractions;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task UpdateCustomer(Expression<Func<Customer, bool>> predicateExpression, string name, string email, string phone);
    Task ChangePassword(Expression<Func<Customer, bool>> predicateExpression, string newPassword);
}