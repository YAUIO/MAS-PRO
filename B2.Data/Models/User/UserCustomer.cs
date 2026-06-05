namespace B2.Data.Models.User;

public partial class User
{
    public class Customer
    {
        public Guid Id { get; init; }
        
        public required DateOnly BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = [];

        public virtual ICollection<Bookmark> Bookmarks { get; set; } = [];
    }
}