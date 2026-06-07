using B2.App.Dtos;

namespace B2.Frontend.Fetchers;

public class OrderFetcher(Fetcher fetcher) : IOrderFetcher
{
    public async Task CreateOrderAsync(CreateOrderDto dto)
    {
        await fetcher.Post("orders", dto);
    }
}