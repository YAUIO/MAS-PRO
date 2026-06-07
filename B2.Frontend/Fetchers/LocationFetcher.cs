using B2.App.Dtos;

namespace B2.Frontend.Fetchers;

public class LocationFetcher(Fetcher fetcher) : ILocationFetcher
{
    public async Task<List<LocationDto>> GetAllUserLocations()
    {
        return await fetcher.GetAtPath<List<LocationDto>>("locations");
    }
}