using B2.Data.Models;

namespace B2.Data.Repositories;

public interface IDeliveryMethodRepository
{
    Task<DeliveryMethod?> GetMethodById(Guid id);
}