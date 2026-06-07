using B2.App.Dtos;

namespace B2.Frontend.Fetchers;

public interface ILocationFetcher
{
    Task<List<LocationDto>> GetAllUserLocations();
}