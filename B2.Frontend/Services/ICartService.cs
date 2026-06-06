using B2.App.Dtos;

namespace B2.Frontend.Services;

public interface ICartService
{
    Task AddToCart(ListingDto dto);
}