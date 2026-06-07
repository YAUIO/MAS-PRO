using B2.Data.Models;

namespace B2.Data.Repositories;

public interface ILocationRepository
{
    Task<List<Location>> GetUserLocations(Guid userId);

    Task<List<Location>> GetSellerPickupPoints(Guid sellerId);

    Task<Location?> GetLocationByIdAsync(Guid id);
}