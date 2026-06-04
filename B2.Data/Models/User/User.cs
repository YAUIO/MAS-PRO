namespace B2.Data.Models.User;

public partial class User
{
    public Guid Id { get; init; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public UserType[] Type { get; private set; }

    public bool IsSeller => Type.Contains(UserType.Seller);

    public bool IsCustomer => Type.Contains(UserType.Customer);

    public Seller? SellerObj { get; set; }
    
    public Customer? CustomerObj { get; set; }

    public void RegisterCustomer(Customer customer)
    {
        
    }

    public void RegisterSeller(Seller seller)
    {
        
    }
    
    public void UnregisterCustomer(Customer customer)
    {
        
    }

    public void UnregisterSeller(Seller seller)
    {
        
    }
}