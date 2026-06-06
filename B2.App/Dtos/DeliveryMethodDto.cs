using B2.Data.Models;

namespace B2.App.Dtos;

public class DeliveryMethodDto
{
    public required string Name { get; init; }
    
    public required bool IsInStore { get; init; }
}

public static class DeliveryMethodMapping
{
    public static DeliveryMethodDto ToDto(this DeliveryMethod obj) => new()
    {
        Name = obj.Name,
        IsInStore = obj.IsInStore,
    };
}