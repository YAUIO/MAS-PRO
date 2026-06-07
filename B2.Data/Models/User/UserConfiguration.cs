using Microsoft.EntityFrameworkCore;
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
            .HasField("_type");

        builder.HasMany(b => b.Locations);

        builder.OwnsOne(b => b.CustomerObj, c =>
        {
            c.HasKey(b => b.Id);
            c.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            c.Property(b => b.BirthDate)
                .IsRequired();
        });
        
        
        builder.OwnsOne(b => b.SellerObj, s =>
        {
            s.HasKey(b => b.Id);
            s.Property(b => b.Id)
                .ValueGeneratedOnAdd();
            
            s.Property(b => b.Name)
                .HasMaxLength(200)
                .IsRequired();
            
            s.Property(b => b.CompanyName)
                .HasMaxLength(200)
                .IsRequired();

            s.Property(b => b.Role)
                .IsRequired();

            s.Navigation(b => b.SupportedDeliveryMethods)
                .AutoInclude();

            s.Navigation(b => b.OwnedLocations)
                .AutoInclude();
        });

        builder.Ignore(b => b.IsCustomer);
        builder.Ignore(b => b.IsSeller);
    }
}