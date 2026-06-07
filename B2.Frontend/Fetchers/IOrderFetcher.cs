using B2.App.Dtos;

namespace B2.Frontend.Fetchers;

public interface IOrderFetcher
{
    Task CreateOrderAsync(CreateOrderDto dto);
}