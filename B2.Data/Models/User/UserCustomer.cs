using System.ComponentModel.DataAnnotations;

namespace B2.Data.Models.User;

public partial class User
{
    public record CustomerCreate(DateOnly BirthDate, List<Order> Orders, List<Bookmark> Bookmarks, Guid? id = null);
    
    public class Customer : IValidatableObject
    {
        public Customer(User user)
        {
            if (user.IsCustomer|| user.CustomerObj != null)
                throw new ValidationException("User is already a customer");

            User = user;
            user.RegisterCustomer(this);
        }
        
        protected Customer(){}
        
        public Guid Id { get; init; }
        
        public required DateOnly BirthDate { get; set; }
        
        protected Guid UserId { get; set; }
        
        public User User { get; protected set; }

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