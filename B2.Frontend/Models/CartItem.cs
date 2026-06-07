using B2.App.Dtos;

namespace B2.Frontend.Models;

public class CartItem
{
    public required ListingDto Product { get; init; }

    public int Amount { get; set; } = 1;
}