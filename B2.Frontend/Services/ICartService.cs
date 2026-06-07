using B2.App.Dtos;
using B2.Frontend.Models;

namespace B2.Frontend.Services;

public interface ICartService
{
    Task AddToCartAsync(ListingDto dto, Guid sellerId);

    Task DeleteFromCart(ListingDto dto, Guid sellerId);

    Task ClearCart();

    Task<List<CartItem>> GetCartAsync();
}