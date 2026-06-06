using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Address : IValidatableObject
{
    public Guid Id { get; init; }
    
    public required string Street { get; set; }
    
    public required string House { get; set; }
    
    public required string City { get; set; }

    public virtual ICollection<Location> RegisteredLocations { get; set; } = [];
    
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (House == null)
            yield return new ValidationResult("Property cannot be null",
                [nameof(House)]);
        
        if (string.IsNullOrEmpty(City))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(City)]);
        
        if (string.IsNullOrEmpty(Street))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(Street)]);
    }
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