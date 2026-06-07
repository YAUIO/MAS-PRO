using B2.Data.DbContext;
using B2.Data.Models.User;
using Microsoft.EntityFrameworkCore;

namespace B2.Data.Repositories;

public class UserRepository(B2DbContext context) : IUserRepository
{
    public async Task<User.Customer?> GetCustomerByUserId(Guid userId)
    {
        var u = await context.Users
                .SingleOrDefaultAsync(s => s.Id == userId);
        return u?.CustomerObj;
    }
}