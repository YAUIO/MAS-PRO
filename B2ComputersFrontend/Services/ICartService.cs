using B2.App.Dtos;

namespace B2ComputersFrontend.Services;

public interface ICartService
{
    Task AddToCart(ListingDto dto);
}