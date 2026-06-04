namespace B2.Data.Models.User;

public partial class User
{
    public class Seller
    {
        public enum SellerRole
        {
            None,
            Owner,
            Manager,
            Employee
        }
        
        public Guid Id { get; init; }
        
        public string Name { get; set; }
        
        public string CompanyName { get; set; }
        
        public SellerRole Role { get; set; }

        public virtual ICollection<Listing> Listings { get; set; } = [];
    }
}