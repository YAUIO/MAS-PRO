using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Location
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Address Address { get; set; }

    public virtual ICollection<User.User.Seller> Sellers { get; set; } = [];
}

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(b => b.Address)
            .WithMany(b => b.RegisteredLocations)
            .IsRequired();

        builder.HasMany(b => b.Sellers)
            .WithMany(b => b.OwnedLocations);
    }
}