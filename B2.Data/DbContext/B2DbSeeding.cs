using B2.Data.Models;
using B2.Data.Models.User;
using Microsoft.EntityFrameworkCore;

namespace B2.Data.DbContext;

public partial class B2DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSeeding((ctx, _) =>
        {
            T Local<T>(Func<T, bool> predicate) where T : class
                => ctx.Set<T>().Local.FirstOrDefault(predicate)
                   ?? ctx.Set<T>().First(predicate);

            if (!ctx.Set<DeliveryMethod>().Any())
                ctx.Set<DeliveryMethod>().AddRange(
                    new DeliveryMethod { Id = Guid.Parse("b9687d95-8f79-40b6-8aa5-8ec711e19d4b"), Name = "Standard Shipping", IsInStore = false },
                    new DeliveryMethod { Id = Guid.Parse("c68d0323-3158-4674-a89e-a3b7d2b5c436"), Name = "In-Store Pickup",    IsInStore = true  },
                    new DeliveryMethod { Id = Guid.Parse("f7b36eb8-dd6b-4f95-8d6e-d94714ec5fd4"), Name = "Express Courier",   IsInStore = false }
                );

            if (!ctx.Set<Category>().Any())
                ctx.Set<Category>().AddRange(
                    new Category { Id = Guid.Parse("a1000000-0000-0000-0000-000000000001"), Name = "Electronics" },
                    new Category { Id = Guid.Parse("a1000000-0000-0000-0000-000000000002"), Name = "Accessories" },
                    new Category { Id = Guid.Parse("a1000000-0000-0000-0000-000000000003"), Name = "Audio"       }
                );

            if (!ctx.Set<Location>().Any())
            {
                var addresses = new[]
                {
                    new Address { Id = Guid.Parse("6d03bb42-183f-4de8-9768-9404f361d764"), City = "New York", Street = "Maple St",      House = "12" },
                    new Address { Id = Guid.Parse("28ef1da7-0c6e-4389-8b95-aedd0a246cad"), City = "New York", Street = "Office Blvd",    House = "5"  },
                    new Address { Id = Guid.Parse("b699d282-9447-421a-be3e-014f3b312b1b"), City = "New York", Street = "High St",        House = "3"  },
                    new Address { Id = Guid.Parse("ac41b09d-fd39-4f2b-82d7-358da6ea2a7f"), City = "New York", Street = "Westgate Mall",  House = "14" },
                    new Address { Id = Guid.Parse("74742dbe-732d-4702-857e-237e30d3ee57"), City = "New York", Street = "Mill Rd",        House = "8"  },
                };
                ctx.Set<Address>().AddRange(addresses);

                ctx.Set<Location>().AddRange(
                    new Location { Id = Guid.Parse("5cae57bf-522a-4672-a9c8-9e173c5543a7"), Name = "Home",                       Address = addresses[0] },
                    new Location { Id = Guid.Parse("fa0e60cc-909d-4d0a-8f9c-c3b065adff48"), Name = "Work",                       Address = addresses[1] },
                    new Location { Id = Guid.Parse("df56f7d2-64c5-46a2-888f-91834bd352eb"), Name = "TechShop – City Centre",     Address = addresses[2] },
                    new Location { Id = Guid.Parse("c73fdac8-28f5-4cc9-9f42-341748fa5657"), Name = "TechShop – Westgate Mall",   Address = addresses[3] },
                    new Location { Id = Guid.Parse("5930f3ee-1e5c-4eac-878b-990df51e6382"), Name = "GadgetHub – North Quarter",  Address = addresses[4] }
                );
            }

            ctx.SaveChanges();

            var dmShipping    = Local<DeliveryMethod>(d => d.Name == "Standard Shipping");
            var dmPickup      = Local<DeliveryMethod>(d => d.IsInStore);
            var dmExpress     = Local<DeliveryMethod>(d => d.Name == "Express Courier");
            var electronics   = Local<Category>(c => c.Name == "Electronics");
            var accessories   = Local<Category>(c => c.Name == "Accessories");
            var audio         = Local<Category>(c => c.Name == "Audio");
            var locHome       = Local<Location>(l => l.Name == "Home");
            var locWork       = Local<Location>(l => l.Name == "Work");
            var locTechCity   = Local<Location>(l => l.Name == "TechShop – City Centre");
            var locTechWest   = Local<Location>(l => l.Name == "TechShop – Westgate Mall");
            var locGadgetNorth = Local<Location>(l => l.Name == "GadgetHub – North Quarter");

            if (!ctx.Set<Product>().Any())
                ctx.Set<Product>().AddRange(
                    new Product
                    {
                        Id = Guid.Parse("b2000000-0000-0000-0000-000000000001"),
                        Name = "UltraBook Pro 15",
                        Description = "Thin and light laptop with a 15\" OLED display.",
                        Category = electronics,
                        Specifications =
                        [
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0001-000000000001"), Name = "RAM",     Value = "16 GB"      },
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0001-000000000002"), Name = "Storage", Value = "512 GB SSD" },
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0001-000000000003"), Name = "Display", Value = "15\" OLED"  },
                        ]
                    },
                    new Product
                    {
                        Id = Guid.Parse("b2000000-0000-0000-0000-000000000002"),
                        Name = "SmartPhone X12",
                        Description = "Flagship smartphone with a 6.7\" display and triple camera.",
                        Category = electronics,
                        Specifications =
                        [
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0002-000000000001"), Name = "Battery", Value = "5000 mAh"      },
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0002-000000000002"), Name = "Camera",  Value = "Triple 108 MP" },
                        ]
                    },
                    new Product
                    {
                        Id = Guid.Parse("b2000000-0000-0000-0000-000000000003"),
                        Name = "ProSound Headphones",
                        Description = "Over-ear noise-cancelling headphones with 40h battery.",
                        Category = audio,
                        Specifications =
                        [
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0003-000000000001"), Name = "Battery Life", Value = "40 hours"       },
                            new Product.Specification { Id = Guid.Parse("b2000000-0000-0000-0003-000000000002"), Name = "Connectivity", Value = "Bluetooth 5.3"  },
                        ]
                    },
                    new Product
                    {
                        Id = Guid.Parse("b2000000-0000-0000-0000-000000000004"),
                        Name = "Universal Laptop Sleeve",
                        Description = "Water-resistant sleeve fitting laptops up to 16\".",
                        Category = accessories,
                    }
                );

            if (!ctx.Set<User>().Any())
            {
                ctx.Set<User>().AddRange(
                    new User("Alice", "Smith", "alice@techshop.com", new User.SellerCreate(
                        Name: "Alice Smith", CompanyName: "TechShop", Role: User.Seller.SellerRole.Owner,
                        methods: [dmShipping, dmPickup, dmExpress],
                        locations: [locTechCity, locTechWest],
                        listings: [],
                        id: Guid.Parse("c3000000-0000-0000-0000-000000000001")))
                    {
                        Id = Guid.Parse("c3100000-0000-0000-0000-000000000001"),
                        Locations = [locTechCity, locTechWest]
                    },
                    new User("Bob", "Jones", "bob@gadgethub.com", new User.SellerCreate(
                        Name: "Bob Jones", CompanyName: "GadgetHub", Role: User.Seller.SellerRole.Owner,
                        methods: [dmShipping, dmPickup],
                        locations: [locGadgetNorth],
                        listings: [],
                        id: Guid.Parse("c3000000-0000-0000-0000-000000000002")))
                    {
                        Id = Guid.Parse("c3200000-0000-0000-0000-000000000002"),
                        Locations = [locGadgetNorth]
                    },
                    new User("Carol", "White", "carol@example.com", customer: new User.CustomerCreate(
                        BirthDate: new DateOnly(1990, 5, 14),
                        Orders: [], Bookmarks: [],
                        id: Guid.Parse("c3000000-0000-0000-0000-000000000003")))
                    {
                        Id = Guid.Parse("c3300000-0000-0000-0000-000000000003"),
                        Locations = [locHome, locWork]
                    }
                );
            }

            ctx.SaveChanges();

            var laptop     = Local<Product>(p => p.Name == "UltraBook Pro 15");
            var phone      = Local<Product>(p => p.Name == "SmartPhone X12");
            var headphones = Local<Product>(p => p.Name == "ProSound Headphones");
            var sleeve     = Local<Product>(p => p.Name == "Universal Laptop Sleeve");
            var alice      = Local<User>(u => u.Email == "alice@techshop.com");
            var bob        = Local<User>(u => u.Email == "bob@gadgethub.com");
            var carol      = Local<User>(u => u.Email == "carol@example.com");

            if (!ctx.Set<Listing>().Any())
                ctx.Set<Listing>().AddRange(
                    new Listing { Id = Guid.Parse("d4000000-0000-0000-0000-000000000001"), Price = 1299.99, Product = laptop,     Seller = alice.SellerObj! },
                    new Listing { Id = Guid.Parse("d4000000-0000-0000-0000-000000000002"), Price = 1199.99, Product = laptop,     Seller = bob.SellerObj!   },
                    new Listing { Id = Guid.Parse("d4000000-0000-0000-0000-000000000003"), Price =  799.99, Product = phone,      Seller = alice.SellerObj! },
                    new Listing { Id = Guid.Parse("d4000000-0000-0000-0000-000000000004"), Price =  749.99, Product = phone,      Seller = bob.SellerObj!   },
                    new Listing { Id = Guid.Parse("d4000000-0000-0000-0000-000000000005"), Price =  249.99, Product = headphones, Seller = alice.SellerObj! },
                    new Listing { Id = Guid.Parse("d4000000-0000-0000-0000-000000000006"), Price =   29.99, Product = sleeve,     Seller = bob.SellerObj!   }
                );

            if (!ctx.Set<Bookmark>().Any())
                ctx.Set<Bookmark>().AddRange(
                    new Bookmark { Id = Guid.Parse("e5000000-0000-0000-0000-000000000001"), Note = "Cheapest laptop option",  Product = laptop,     Customer = carol.CustomerObj! },
                    new Bookmark { Id = Guid.Parse("e5000000-0000-0000-0000-000000000002"), Note = "Great reviews on forums", Product = headphones, Customer = carol.CustomerObj! }
                );

            if (!ctx.Set<Order>().Any())
            {
                var listingAliceLaptop = Local<Listing>(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000001"));
                var listingAlicePhone  = Local<Listing>(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000003"));
                var listingBobSleeve   = Local<Listing>(l => l.Id == Guid.Parse("d4000000-0000-0000-0000-000000000006"));

                ctx.Set<Order>().AddRange(
                    new Order
                    {
                        Id = Guid.Parse("f6000000-0000-0000-0000-000000000001"),
                        PlacedAt = DateTime.UtcNow.AddDays(-10),
                        Status = Order.OrderStatus.Delivered,
                        Customer = carol.CustomerObj!,
                        DeliveryMethod = dmShipping,
                        Location = locHome,
                        Purchases =
                        [
                            new Purchase { Id = Guid.Parse("f6000000-0000-0001-0000-000000000001"), Amount = 1, Listing = listingAliceLaptop },
                            new Purchase { Id = Guid.Parse("f6000000-0000-0001-0000-000000000002"), Amount = 2, Listing = listingBobSleeve   },
                        ]
                    }
                );
            }

            ctx.SaveChanges();
        });
    }
}