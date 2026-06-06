using B2.Data.Models;

namespace B2.App.Dtos;

public class ListingDto
{
    public required ProductDto Product { get; init; }
    
    public required SellerDto Seller { get; init; }
    
    public required double Price { get; init; }
}

public static class ListingMapping
{
    public static ListingDto ToDto(this Listing obj) => new()
    {
        Product = obj.Product.ToDto(),
        Seller = obj.Seller.ToDto(),
        Price = obj.Price,
    };
}