using B2.Data.Models;

namespace B2.Data.Repositories;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
}