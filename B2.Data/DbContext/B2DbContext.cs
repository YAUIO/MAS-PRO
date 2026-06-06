using System.ComponentModel.DataAnnotations;
using B2.Data.Models;
using B2.Data.Models.User;
using Microsoft.EntityFrameworkCore;

namespace B2.Data.DbContext;

internal class B2DbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    public DbSet<User.Customer> Customers => Set<User.Customer>();
    
    public DbSet<User.Seller> Sellers => Set<User.Seller>();
    
    public DbSet<Address> Addresses => Set<Address>();
    
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
    
    public DbSet<Category> Categories => Set<Category>();
    
    public DbSet<DeliveryMethod> DeliveryMethods => Set<DeliveryMethod>();
    
    public DbSet<Listing> Listings => Set<Listing>();
    
    public DbSet<Location> Locations => Set<Location>();
    
    public DbSet<Order> Orders => Set<Order>();
    
    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Purchase> Purchases => Set<Purchase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    public override int SaveChanges()
    {
        Validate();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        Validate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void Validate()
    {
        var validationErrors = ChangeTracker
            .Entries<IValidatableObject>()
            .SelectMany(e => e.Entity.Validate(null))
            .Where(r => r != ValidationResult.Success);

        var msgs = validationErrors.Select(v => v.ErrorMessage);

        if(validationErrors.Any())
        {
            throw new ValidationException($"Invalid object state on save: {string.Join(", ", msgs)}");
        }
    }
}