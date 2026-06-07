using B2.Data.DbContext;
using B2.Data.Models.User;

namespace B2.Data.Repositories;

public class UserRepository(B2DbContext context) : IUserRepository
{
    public async Task<User.Customer?> GetCustomerByUserId(Guid userId)
    {
        return await context.Customers.FindAsync(userId);
    }
}