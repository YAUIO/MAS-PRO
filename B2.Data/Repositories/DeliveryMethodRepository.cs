using B2.Data.DbContext;
using B2.Data.Models;

namespace B2.Data.Repositories;

public class DeliveryMethodRepository(B2DbContext context) : IDeliveryMethodRepository
{
    public async Task<DeliveryMethod?> GetMethodById(Guid id)
    {
        return await context.DeliveryMethods.FindAsync(id);
    }
}