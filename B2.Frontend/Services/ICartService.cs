using B2.App.Dtos;
using B2.Frontend.Models;

namespace B2.Frontend.Services;

public interface ICartService
{
    Task AddToCartAsync(ListingDto dto);

    Task DeleteFromCart(ListingDto dto);

    Task ClearCart();

    Task<List<CartItem>> GetCartAsync();
}