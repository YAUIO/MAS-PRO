namespace B2.Data.Models;

public class Category
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
}