using B2.Data.Models;

namespace B2.App.Dtos;

public class ListingDto
{
    public Guid Id { get; init; }
    
    public required Guid ProductId { get; init; }
    
    public required SellerDto Seller { get; init; }
    
    public required double Price { get; init; }
}

public static class ListingMapping
{
    public static ListingDto ToDto(this Listing obj, ProductDto? dto = null) => new()
    {
        Id = obj.Id,
        ProductId = dto?.Id ?? obj.Product.ToDto().Id,
        Seller = obj.Seller.ToDto(),
        Price = obj.Price,
    };
}