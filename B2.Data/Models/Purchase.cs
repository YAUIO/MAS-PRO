namespace B2.Data.Models;

public class Purchase
{
    public Guid Id { get; init; }
    
    public required int Amount { get; set; }

    public required Listing Listing { get; set; }
    
    public required Order Order { get; set; }
}