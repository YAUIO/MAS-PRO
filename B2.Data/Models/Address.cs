using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Address
{
    public Guid Id { get; init; }
    
    public required string Street { get; set; }
    
    public required string House { get; set; }
    
    public required string City { get; set; }

    public virtual ICollection<Location> RegisteredLocations { get; set; } = [];
}

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Street)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(b => b.House)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(b => b.City)
            .HasMaxLength(200)
            .IsRequired();
    }
}