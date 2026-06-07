using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using B2.Data.DbContext;

namespace B2.Data.Models.User;

public partial class User : IValidatableObject
{
    private HashSet<UserType> _type = [];
    
    public Guid Id { get; init; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }

    public IReadOnlySet<UserType> Type => _type.AsReadOnly();

    public bool IsSeller => Type.Contains(UserType.Seller);

    public bool IsCustomer => Type.Contains(UserType.Customer);

    public Seller? SellerObj { get; private set; }
    
    public Customer? CustomerObj { get; private set; }

    public virtual ICollection<Location> Locations { get; set; } = [];

    [SetsRequiredMembers]
    public User(string firstName, string lastName, string email, SellerCreate? seller = null, CustomerCreate? customer = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        if (seller is null && customer is null)
            throw new ConstraintException("Both seller and customer can't be null");

        if (seller is not null)
        {
            var sellerObj = new Seller(this)
            {
                Id = seller.id ?? Guid.NewGuid(),
                CompanyName = seller.CompanyName,
                Name = seller.Name,
                Listings = seller.listings,
                Role = seller.Role,
                OwnedLocations = seller.locations,
                SupportedDeliveryMethods = seller.methods
            };
            RegisterSeller(sellerObj);
        }

        if (customer is not null)
        {
            var customerObj = new Customer(this)
            {
                Id = customer.id ?? Guid.NewGuid(),
                BirthDate = customer.BirthDate,
                Bookmarks = customer.Bookmarks,
                Orders = customer.Orders,
            };
            RegisterCustomer(customerObj);
        }
    }

    protected User()
    {
        
    }

    public void RegisterCustomer(Customer customer)
    {
        if (CustomerObj == customer) return;
        
        if (customer == null)
            throw new ConstraintException("Customer is null");
        
        if (CustomerObj != null || !_type.Add(UserType.Customer))
            throw new ConstraintException("Customer already registered");

        CustomerObj = customer;
    }

    public void RegisterSeller(Seller seller)
    {
        if (SellerObj == seller) return;
        
        if (seller == null)
            throw new ConstraintException("Seller is null");
        
        if (SellerObj != null || !_type.Add(UserType.Seller))
            throw new ConstraintException("Seller already registered");

        SellerObj = seller;
    }
    
    public void UnregisterCustomer(Customer customer, B2DbContext context)
    {
        if (SellerObj == null)
            throw new ConstraintException("Both customer and seller cannot be null");
        
        if (customer == null)
            throw new ConstraintException("Customer is null");
        
        if (CustomerObj == null || !_type.Remove(UserType.Customer))
            throw new ConstraintException("Customer already unregistered");

        CustomerObj = null;

        context.Remove(customer);
    }

    public void UnregisterSeller(Seller seller, B2DbContext context)
    {
        if (CustomerObj == null)
            throw new ConstraintException("Both customer and seller cannot be null");
        
        if (seller == null)
            throw new ConstraintException("Seller is null");
        
        if (SellerObj == null || !_type.Remove(UserType.Seller))
            throw new ConstraintException("Seller already unregistered");

        SellerObj = null;

        context.Remove(seller);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(FirstName))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(FirstName)]);
        
        if (string.IsNullOrEmpty(LastName))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(LastName)]);
        
        if (string.IsNullOrEmpty(Email))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(Email)]);
        
        if (CustomerObj == null && SellerObj == null)
            yield return new ValidationResult("Both cannot be null",
                [nameof(CustomerObj), nameof(SellerObj)]);
    }
}