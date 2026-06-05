namespace B2.Data.Models;

public class DeliveryMethod
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required bool IsInStore { get; set; }
}