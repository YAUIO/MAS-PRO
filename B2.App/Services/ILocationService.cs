using B2.App.Dtos;

namespace B2.App.Services;

public interface ILocationService
{
    Task<List<LocationDto>> GetAllSellerLocationsAsync(Guid sellerId);
    Task<List<LocationDto>> GetAllUserLocationsAsync(Guid userId);
}