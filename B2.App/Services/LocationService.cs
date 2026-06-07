using B2.App.Dtos;
using B2.Data.Repositories;

namespace B2.App.Services;

public class LocationService(ILocationRepository repo) : ILocationService
{
    public async Task<List<LocationDto>> GetAllSellerLocationsAsync(Guid sellerId)
    {
        var pp = await repo.GetSellerPickupPoints(sellerId);
        return [.. pp.Select(l => l.ToDto())];
    }

    public async Task<List<LocationDto>> GetAllUserLocationsAsync(Guid userId)
    {
        var pp = await repo.GetUserLocations(userId);
        return [.. pp.Select(l => l.ToDto())];
    }
}