using B2.App.Dtos;
using B2.App.Exceptions;
using B2.Data.Models;
using B2.Data.Repositories;
using Microsoft.Extensions.Configuration;

namespace B2.App.Services;

public class OrderService(
    IOrderRepository repo, 
    IUserRepository users, 
    ILocationRepository locations, 
    IListingRepository listings, 
    IDeliveryMethodRepository delivery, 
    IConfiguration cfg) : IOrderService
{
    public async Task CreateOrderAsync(CreateOrderDto dto)
    {
        var user = await users.GetCustomerByUserId(Guid.Parse(cfg["userId"]!))
            ?? throw new BadRequestException("No such user");
        
        var del = await delivery.GetMethodById(dto.DeliveryMethodId)
                  ?? throw new BadRequestException("No such method");

        List<Purchase> purchases =
        [
            .. dto.Purchases.Select(p =>
            {
                var listing = listings.GetListingById(p.ListingId) ?? throw new BadRequestException($"No such listing {p.ListingId}");
                return new Purchase()
                {
                    Amount = p.Amount,
                    Listing = listing,
                };
            })
        ];

        var location = await locations.GetLocationByIdAsync(dto.Location.Id) 
            ?? throw new BadRequestException($"No such location {dto.Location.Address}");
        
        var model = new Order
        {
            PlacedAt = DateTime.UtcNow,
            Status = Order.OrderStatus.Created,
            Customer = user,
            DeliveryMethod = del,
            Purchases = purchases,
            Location = location,
        };
        await repo.AddOrderAsync(model);
    }
}