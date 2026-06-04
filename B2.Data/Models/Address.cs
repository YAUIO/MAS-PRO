namespace B2.Data.Models;

public class Address
{
    public Guid Id { get; init; }
    
    public string Street { get; set; }
    
    public string House { get; set; }
    
    public string City { get; set; }
}