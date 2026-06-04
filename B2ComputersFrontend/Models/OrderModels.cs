namespace B2ComputersFrontend.Models;

public class CartItem
{
    public string ProductName { get; set; } = "";
    public string SellerName { get; set; } = "";
    public decimal Price { get; set; }
}

public class DeliveryMethod
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsInStore { get; set; }
    // Which seller IDs support this method
    public List<string> SupportedSellerIds { get; set; } = new();
}

public class Address
{
    public int Id { get; set; }
    public string Label { get; set; } = "";
}

public class PickupPoint
{
    public int Id { get; set; }
    public string SellerId { get; set; } = "";
    public string Label { get; set; } = "";
}
