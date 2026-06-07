using B2.Data.Models.User;

namespace B2.Data.Repositories;

public interface IUserRepository
{
    Task<User.Customer?> GetCustomerByUserId(Guid userId);
}