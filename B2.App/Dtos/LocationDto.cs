using B2.Data.Models;

namespace B2.App.Dtos;

public class LocationDto
{
    public required string Name { get; init; }
    
    public required string Address { get; init; }
}

public static class LocationMapping
{
    public static LocationDto ToDto(this Location obj) => new()
    {
        Name = obj.Name,
        Address = $"{obj.Address.City}, {obj.Address.Street} {obj.Address.House}"
    };
}