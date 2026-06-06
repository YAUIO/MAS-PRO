using B2.Data.Models;
using B2.Data.Models.User;

namespace B2.App.Dtos;

public class SellerDto
{
    public Guid Id { get; init; }
        
    public required string Name { get; set; }
        
    public required string CompanyName { get; set; }
        
    public required User.Seller.SellerRole Role { get; set; }

    public List<ListingDto> Listings { get; set; } = [];

    public List<DeliveryMethodDto> SupportedDeliveryMethods { get; set; } = [];
}

public static class SellerMapping
{
    public static SellerDto ToDto(this User.Seller obj) => new()
    {
        Name = obj.Name,
        CompanyName = obj.CompanyName,
        Role = User.Seller.SellerRole.Owner,
        Listings = [.. obj.Listings.Select(o => o.ToDto())],
        SupportedDeliveryMethods = [.. obj.SupportedDeliveryMethods.Select(o => o.ToDto())]
    };
}