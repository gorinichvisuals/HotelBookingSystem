namespace HotelBooking.Infrastructure.Repositories.Implementations;

internal sealed class CustomerRepository(BookingContext context) : ICustomerRepository
{
    public BookingContext Context { get; set; } = context;

    public async Task UpdateCustomer(Expression<Func<Customer, bool>> predicateExpression, string name, string email, string phone)
    {
        await Context.Customers.Where(predicateExpression)
            .ExecuteUpdateAsync(c => 
                c.SetProperty(c => c.Name, name)
                .SetProperty(c => c.Email, email)
                .SetProperty(c => c.Phone, phone));
    }

    public async Task ChangePassword(Expression<Func<Customer, bool>> predicateExpression, string newPassword)
    {
        await Context.Customers.Where(predicateExpression)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(c => c.Name, newPassword));
    }
}