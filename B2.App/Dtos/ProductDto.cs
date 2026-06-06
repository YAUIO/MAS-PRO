using B2.Data.Models;

namespace B2.App.Dtos;

public class ProductDto
{
    public Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required string Category { get; init; }

    public List<SpecificationDto> Specifications { get; init; } = [];
    
    public List<ListingDto> Listings { get; init; } = [];
}

public static class ProductMapping
{
    public static ProductDto ToDto(this Product obj) => new()
    {
        Id = obj.Id,
        Name = obj.Name,
        Description = obj.Description,
        Category = obj.Category.Name,
        Listings = [.. obj.Listings.Select(o => o.ToDto())],
        Specifications = [.. obj.Specifications.Select(o => o.ToDto())],
    };
}