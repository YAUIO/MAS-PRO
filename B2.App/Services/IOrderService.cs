using B2.App.Dtos;

namespace B2.App.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrderDto dto);
}