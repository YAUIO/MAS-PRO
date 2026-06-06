namespace B2.App.Dtos;

public class ListingDto
{
    public ProductDto Product { get; init; }
    
    public SellerDto Seller { get; init; }
    
    public double Price { get; init; }
}