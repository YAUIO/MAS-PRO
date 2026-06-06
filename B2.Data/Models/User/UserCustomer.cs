using System.ComponentModel.DataAnnotations;

namespace B2.Data.Models.User;

public partial class User
{
    public class Customer : IValidatableObject
    {
        public Guid Id { get; init; }
        
        public required DateOnly BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = [];

        public virtual ICollection<Bookmark> Bookmarks { get; set; } = [];
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BirthDate > DateOnly.FromDateTime(DateTime.Today))
                yield return new ValidationResult("Property cannot in the future",
                    [nameof(BirthDate)]);
        }
    }
}