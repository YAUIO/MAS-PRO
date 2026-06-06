using System.ComponentModel.DataAnnotations;

namespace B2.Data.Models.User;

public partial class User
{
    public class Seller : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SupportedDeliveryMethods.Count == 0)
                yield return new ValidationResult("Collection cannot be empty",
                    [nameof(SupportedDeliveryMethods)]);
        }
    }
}