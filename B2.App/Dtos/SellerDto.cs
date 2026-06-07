using B2.Data.Models.User;

namespace B2.App.Dtos;

public class SellerDto
{
    public Guid Id { get; init; }
        
    public required string Name { get; set; }
        
    public required string CompanyName { get; set; }
        
    public required User.Seller.SellerRole Role { get; set; }

    public List<DeliveryMethodDto> SupportedDeliveryMethods { get; set; } = [];

    public List<LocationDto> PickupPoints { get; set; } = [];
}

public static class SellerMapping
{
    public static SellerDto ToDto(this User.Seller obj)
    {
        var dto = new SellerDto()
        {
            Id = obj.Id,
            Name = obj.Name,
            CompanyName = obj.CompanyName,
            Role = User.Seller.SellerRole.Owner,
            SupportedDeliveryMethods = [.. obj.SupportedDeliveryMethods.Select(o => o.ToDto())],
            PickupPoints = [.. obj.OwnedLocations.Select(l => l.ToDto())]
        };
        return dto;
    }
}