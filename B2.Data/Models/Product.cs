namespace B2.Data.Models;

public class Product
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required Category Category { get; set; }

    public virtual ICollection<Specification> Specifications { get; set; } = [];
    
    public virtual ICollection<Listing> Listings { get; set; } = [];
    
    public class Specification
    {
        public Guid Id { get; init; }
    
        public required string Name { get; set; }
    
        public required string Value { get; set; }
    }
}