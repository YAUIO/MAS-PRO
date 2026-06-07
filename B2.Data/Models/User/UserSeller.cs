using System.ComponentModel.DataAnnotations;

namespace B2.Data.Models.User;

public partial class User
{
    public record SellerCreate(string Name, 
        string CompanyName, 
        Seller.SellerRole Role, 
        List<DeliveryMethod> methods, 
        List<Location> locations, 
        List<Listing> listings,
        Guid? id = null);
    
    public class Seller : IValidatableObject
    {
        public enum SellerRole
        {
            Owner,
            Manager,
            Employee
        }

        public Seller(User user)
        {
            if (user.IsSeller|| user.SellerObj != null)
                throw new ValidationException("User is already a seller");

            User = user;
            user.RegisterSeller(this);
        }
        
        protected Seller(){}
        
        public Guid Id { get; init; }
        
        public required string Name { get; set; }
        
        public required string CompanyName { get; set; }
        
        public required SellerRole Role { get; set; }
        
        protected Guid UserId { get; set; }
        
        public User User { get; protected set; }

        public virtual ICollection<Listing> Listings { get; set; } = [];
        
        public virtual ICollection<Location> OwnedLocations { get; set; } = [];
        
        public virtual ICollection<DeliveryMethod> SupportedDeliveryMethods { get; set; } = [];

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SupportedDeliveryMethods.Count == 0)
                yield return new ValidationResult("Collection cannot be empty",
                    [nameof(SupportedDeliveryMethods)]);
            
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("Property cannot be empty",
                    [nameof(Name)]);
            
            if (string.IsNullOrEmpty(CompanyName))
                yield return new ValidationResult("Property cannot be empty",
                    [nameof(CompanyName)]);
        }
    }
}