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