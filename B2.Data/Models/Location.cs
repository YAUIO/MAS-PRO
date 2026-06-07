using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Location : IValidatableObject
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Address Address { get; set; }

    public virtual ICollection<User.User.Seller> Sellers { get; set; } = [];
    
    public virtual ICollection<Order> Orders { get; set; } = [];
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(Name)]);
    }
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

        builder.Navigation(b => b.Address)
            .AutoInclude();
    }
}