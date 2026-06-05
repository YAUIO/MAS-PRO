namespace B2.Data.Models;

public class Address
{
    public Guid Id { get; init; }
    
    public required string Street { get; set; }
    
    public required string House { get; set; }
    
    public required string City { get; set; }
}