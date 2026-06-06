using B2.Data.Models;

namespace B2.App.Dtos;

public class SpecificationDto
{
    public required string Key { get; init; }
    
    public required string Value { get; init; }
}

public static class SpecificationMapping
{
    public static SpecificationDto ToDto(this Product.Specification obj) => new()
    {
        Key = obj.Name,
        Value = obj.Value
    };
}