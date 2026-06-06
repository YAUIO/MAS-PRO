using B2.Data.Models;
using B2.Data.Models.User;
using Microsoft.EntityFrameworkCore;

namespace B2.Data.DbContext;

internal class B2DbContext : Microsoft.EntityFrameworkCore.DbContext
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
}