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
            .SelectMany(e => e.Entity.Validate(new ValidationContext(e.Entity, serviceProvider: null, items: null)))
            .Where(r => r != ValidationResult.Success)
            .Select(r => r.ErrorMessage)
            .ToList();

        if (validationErrors.Count != 0)
            throw new ValidationException($"Invalid object state on save: {string.Join(", ", validationErrors)}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSeeding((ctx, _) =>
        {
            if (!ctx.Set<DeliveryMethod>().Any())
                DeliveryMethods.AddRange(new DeliveryMethod
                {
                    Id = Guid.Parse("b9687d95-8f79-40b6-8aa5-8ec711e19d4b"), Name = "Standard Shipping",
                    IsInStore = false
                }, new DeliveryMethod
                {
                    Id = Guid.Parse("c68d0323-3158-4674-a89e-a3b7d2b5c436"), Name = "In-Store Pickup",
                    IsInStore = true
                }, new DeliveryMethod
                {
                    Id = Guid.Parse("f7b36eb8-dd6b-4f95-8d6e-d94714ec5fd4"), Name = "Express Courier",
                    IsInStore = false
                });
                
            if (!ctx.Set<Category>().Any())
            {
                var catElectronics = new Category
                    { Id = Guid.Parse("a1000000-0000-0000-0000-000000000001"), Name = "Electronics" };
                var catAccessories = new Category
                    { Id = Guid.Parse("a1000000-0000-0000-0000-000000000002"), Name = "Accessories" };
                var catAudio = new Category { Id = Guid.Parse("a1000000-0000-0000-0000-000000000003"), Name = "Audio" };
                ctx.Set<Category>().AddRange(catElectronics, catAccessories, catAudio);
            }

            var electronics = ctx.Set<Category>().Local.First(c => c.Name == "Electronics");
            var accessories = ctx.Set<Category>().Local.First(c => c.Name == "Accessories");
            var audio = ctx.Set<Category>().Local.First(c => c.Name == "Audio");
            
            if (!ctx.Set<Product>().Any())
            {
                var prodLaptop = new Product
                {
                    Id = Guid.Parse("b2000000-0000-0000-0000-000000000001"),
                    Name = "UltraBook Pro 15",
                    Description = "Thin and light laptop with a 15\" OLED display.",
                    Category = electronics,
                    Specifications =
                    [
                        new Product.Specification
                            { Id = Guid.Parse("b2000000-0000-0000-0001-000000000001"), Name = "RAM", Value = "16 GB" },
                        new Product.Specification
                        {
                            Id = Guid.Parse("b2000000-0000-0000-0001-000000000002"), Name = "Storage",
                            Value = "512 GB SSD"
                        },
                        new Product.Specification
                        {
                            Id = Guid.Parse("b2000000-0000-0000-0001-000000000003"), Name = "Display",
                            Value = "15\" OLED"
                        }
                    ]
                };

                var prodPhone = new Product
                {
                    Id = Guid.Parse("b2000000-0000-0000-0000-000000000002"),
                    Name = "SmartPhone X12",
                    Description = "Flagship smartphone with a 6.7\" display and triple camera.",
                    Category = electronics,
                    Specifications =
                    [
                        new Product.Specification
                        {
                            Id = Guid.Parse("b2000000-0000-0000-0002-000000000001"), Name = "Battery",
                            Value = "5000 mAh"
                        },
                        new Product.Specification
                        {
                            Id = Guid.Parse("b2000000-0000-0000-0002-000000000002"), Name = "Camera",
                            Value = "Triple 108 MP"
                        }
                    ]
                };

                var prodHeadphones = new Product
                {
                    Id = Guid.Parse("b2000000-0000-0000-0000-000000000003"),
                    Name = "ProSound Headphones",
                    Description = "Over-ear noise-cancelling headphones with 40h battery.",
                    Category = audio,
                    Specifications =
                    [
                        new Product.Specification
                        {
                            Id = Guid.Parse("b2000000-0000-0000-0003-000000000001"), Name = "Battery Life",
                            Value = "40 hours"
                        },
                        new Product.Specification
                        {
                            Id = Guid.Parse("b2000000-0000-0000-0003-000000000002"), Name = "Connectivity",
                            Value = "Bluetooth 5.3"
                        }
                    ]
                };

                var prodCase = new Product
                {
                    Id = Guid.Parse("b2000000-0000-0000-0000-000000000004"),
                    Name = "Universal Laptop Sleeve",
                    Description = "Water-resistant sleeve fitting laptops up to 16\".",
                    Category = accessories
                };

                ctx.Set<Product>().AddRange(prodLaptop, prodPhone, prodHeadphones, prodCase);
            }

            var laptop = ctx.Set<Product>().Local.First(p => p.Name == "UltraBook Pro 15");
            var phone = ctx.Set<Product>().Local.First(p => p.Name == "SmartPhone X12");
            var headphones = ctx.Set<Product>().Local.First(p => p.Name == "ProSound Headphones");
            var sleeve = ctx.Set<Product>().Local.First(p => p.Name == "Universal Laptop Sleeve");
            
            var dmShipping = ctx.Set<DeliveryMethod>().Local.First(d => !d.IsInStore && d.Name == "Standard Shipping");
            var dmPickup = ctx.Set<DeliveryMethod>().Local.First(d => d.IsInStore);
            var dmExpress = ctx.Set<DeliveryMethod>().Local.First(d => !d.IsInStore && d.Name == "Express Courier");
            
            var locHome = ctx.Set<Location>().Local.First(l => l.Name == "Home");
            var locWork = ctx.Set<Location>().Local.First(l => l.Name == "Work");
            var locTechCity = ctx.Set<Location>().Local.First(l => l.Name == "TechShop – City Centre");
            var locTechWest = ctx.Set<Location>().Local.First(l => l.Name == "TechShop – Westgate Mall");
            var locGadgetNorth = ctx.Set<Location>().Local.First(l => l.Name == "GadgetHub – North Quarter");
            
            if (!ctx.Set<User>().Any())
            {
                var sellerObjTech = new User.Seller
                {
                    Id = Guid.Parse("c3000000-0000-0000-0000-000000000001"),
                    Name = "Alice Smith",
                    CompanyName = "TechShop",
                    Role = User.Seller.SellerRole.Owner,
                    SupportedDeliveryMethods = [dmShipping, dmPickup, dmExpress],
                    OwnedLocations = [locTechCity, locTechWest]
                };

                var sellerObjGadget = new User.Seller
                {
                    Id = Guid.Parse("c3000000-0000-0000-0000-000000000002"),
                    Name = "Bob Jones",
                    CompanyName = "GadgetHub",
                    Role = User.Seller.SellerRole.Owner,
                    SupportedDeliveryMethods = [dmShipping, dmPickup],
                    OwnedLocations = [locGadgetNorth]
                };

                var customerObj = new User.Customer
                {
                    Id = Guid.Parse("c3000000-0000-0000-0000-000000000003"),
                    BirthDate = new DateOnly(1990, 5, 14)
                };

                var userAlice = new User("Alice", "Smith", "alice@techshop.com", sellerObjTech, null)
                {
                    Locations = [locTechCity, locTechWest]
                };

                var userBob = new User("Bob", "Jones", "bob@gadgethub.com", sellerObjGadget, null)
                {
                    Locations = [locGadgetNorth]
                };
                
                var userCarol = new User("Carol", "White", "carol@example.com", null, customerObj)
                {
                    Locations = [locHome, locWork]
                };

                ctx.Set<User>().AddRange(userAlice, userBob, userCarol);
            }

            var alice = ctx.Set<User>().Local.First(u => u.Email == "alice@techshop.com");
            var bob = ctx.Set<User>().Local.First(u => u.Email == "bob@gadgethub.com");
            var carol = ctx.Set<User>().Local.First(u => u.Email == "carol@example.com");
            
            if (!ctx.Set<Listing>().Any())
                ctx.Set<Listing>().AddRange(
                    new Listing
                    {
                        Id = Guid.Parse("d4000000-0000-0000-0000-000000000001"), Price = 1299.99, Product = laptop,
                        Seller = alice.SellerObj!
                    },
                    new Listing
                    {
                        Id = Guid.Parse("d4000000-0000-0000-0000-000000000002"), Price = 1199.99, Product = laptop,
                        Seller = bob.SellerObj!
                    },
                    new Listing
                    {
                        Id = Guid.Parse("d4000000-0000-0000-0000-000000000003"), Price = 799.99, Product = phone,
                        Seller = alice.SellerObj!
                    },
                    new Listing
                    {
                        Id = Guid.Parse("d4000000-0000-0000-0000-000000000004"), Price = 749.99, Product = phone,
                        Seller = bob.SellerObj!
                    },
                    new Listing
                    {
                        Id = Guid.Parse("d4000000-0000-0000-0000-000000000005"), Price = 249.99, Product = headphones,
                        Seller = alice.SellerObj!
                    },
                    new Listing
                    {
                        Id = Guid.Parse("d4000000-0000-0000-0000-000000000006"), Price = 29.99, Product = sleeve,
                        Seller = bob.SellerObj!
                    }
                );

            var listingAliceLaptop = ctx.Set<Listing>().Local
                .First(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000001"));
            var listingAlicePhone = ctx.Set<Listing>().Local
                .First(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000003"));
            var listingBobHeadphone = ctx.Set<Listing>().Local
                .First(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000005"));
            var listingBobSleeve = ctx.Set<Listing>().Local
                .First(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000006"));
            
            if (!ctx.Set<Bookmark>().Any())
                ctx.Set<Bookmark>().AddRange(
                    new Bookmark
                    {
                        Id = Guid.Parse("e5000000-0000-0000-0000-000000000001"), Note = "Cheapest laptop option",
                        Product = laptop, Customer = carol.CustomerObj!
                    },
                    new Bookmark
                    {
                        Id = Guid.Parse("e5000000-0000-0000-0000-000000000002"), Note = "Great reviews on forums",
                        Product = headphones, Customer = carol.CustomerObj!
                    }
                );
            
            if (!ctx.Set<Order>().Any())
            {
                var order1 = new Order
                {
                    Id = Guid.Parse("f6000000-0000-0000-0000-000000000001"),
                    PlacedAt = DateTime.UtcNow.AddDays(-10),
                    Status = Order.OrderStatus.Delivered,
                    Customer = carol.CustomerObj!,
                    DeliveryMethod = dmShipping,
                    Purchases =
                    [
                        new Purchase
                        {
                            Id = Guid.Parse("f6000000-0000-0001-0000-000000000001"), Amount = 1,
                            Listing = listingAliceLaptop
                        },
                        new Purchase
                        {
                            Id = Guid.Parse("f6000000-0000-0001-0000-000000000002"), Amount = 2,
                            Listing = listingBobSleeve
                        }
                    ]
                };

                var order2 = new Order
                {
                    Id = Guid.Parse("f6000000-0000-0000-0000-000000000002"),
                    PlacedAt = DateTime.UtcNow.AddDays(-2),
                    Status = Order.OrderStatus.AwaitingPickup,
                    Customer = carol.CustomerObj!,
                    DeliveryMethod = dmPickup,
                    Purchases =
                    [
                        new Purchase
                        {
                            Id = Guid.Parse("f6000000-0000-0002-0000-000000000001"), Amount = 1,
                            Listing = listingAlicePhone
                        }
                    ]
                };

                ctx.Set<Order>().AddRange(order1, order2);
            }

            ctx.SaveChanges();
        });
    }
}