namespace B2.Data.Models.User;

public partial class User
{
    public class Seller
    {
        public enum SellerRole
        {
            Owner,
            Manager,
            Employee
        }
        
        public Guid Id { get; init; }
        
        public required string Name { get; set; }
        
        public required string CompanyName { get; set; }
        
        public required SellerRole Role { get; set; }

        public virtual ICollection<Listing> Listings { get; set; } = [];
        
        public virtual ICollection<Location> OwnedLocations { get; set; } = [];

        public virtual ICollection<DeliveryMethod> SupportedDeliveryMethods { get; set; } = [];
    }
}