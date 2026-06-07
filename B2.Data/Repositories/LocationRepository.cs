using B2.Data.DbContext;
using B2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace B2.Data.Repositories;

public class LocationRepository(B2DbContext context) : ILocationRepository
{
    public async Task<List<Location>> GetUserLocations(Guid userId)
    {
        return await context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Locations)
            .ToListAsync();
    }

    public async Task<List<Location>> GetSellerPickupPoints(Guid sellerId)
    {
        return await context.Users
            .Select(u => u.SellerObj)
            .Where(s => s != null)
            .Where(u => u.Id == sellerId)
            .SelectMany(u => u.OwnedLocations)
            .ToListAsync();
    }

    public async Task<Location?> GetLocationByIdAsync(Guid id)
    {
        return await context.Locations.FindAsync(id);
    }
}