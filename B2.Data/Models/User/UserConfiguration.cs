using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models.User;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Type)
            .HasField("_type")
            .HasConversion(
                v => string.Join(',', v.Select(t => (int)t)),
                v => new HashSet<User.UserType>(v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => (User.UserType)int.Parse(t)))
            );

        builder.HasMany(b => b.Locations);
        
        builder.HasOne(b => b.SellerObj)
            .WithOne(b => b.User)
            .HasForeignKey<User.Seller>("UserId");
        
        builder.HasOne(b => b.CustomerObj)
            .WithOne(b => b.User)
            .HasForeignKey<User.Customer>("UserId");

        builder.Navigation(b => b.CustomerObj)
            .AutoInclude();

        builder.Navigation(b => b.SellerObj)
            .AutoInclude();

        builder.HasMany(b => b.Locations)
            .WithMany();

        builder.Navigation(b => b.Locations)
            .AutoInclude();

        builder.Ignore(b => b.IsCustomer);
        builder.Ignore(b => b.IsSeller);
    }
}

public class SellerConfiguration : IEntityTypeConfiguration<User.Seller>
{
    public void Configure(EntityTypeBuilder<User.Seller> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.CompanyName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Role)
            .IsRequired();

        builder.Navigation(b => b.SupportedDeliveryMethods)
            .AutoInclude();

        builder.Navigation(b => b.OwnedLocations)
            .AutoInclude();
    }
}

public class CustomerConfiguration : IEntityTypeConfiguration<User.Customer>
{
    public void Configure(EntityTypeBuilder<User.Customer> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.BirthDate)
            .IsRequired();
    }
}