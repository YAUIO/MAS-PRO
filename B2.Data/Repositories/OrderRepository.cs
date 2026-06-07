using B2.Data.DbContext;
using B2.Data.Models;

namespace B2.Data.Repositories;

public class OrderRepository(B2DbContext context) : IOrderRepository
{
    public async Task AddOrderAsync(Order order)
    {
        await context.Orders.AddAsync(order);
    }
}