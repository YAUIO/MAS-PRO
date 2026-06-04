namespace B2.Data.Models.User;

public partial class User
{
    public class Customer
    {
        public Guid Id { get; init; }
        
        public DateOnly BirthDate { get; set; }
    }
}