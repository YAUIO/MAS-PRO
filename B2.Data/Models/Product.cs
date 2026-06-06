using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Product : IValidatableObject
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required Category Category { get; set; }

    public virtual ICollection<Specification> Specifications { get; set; } = [];
    
    public virtual ICollection<Listing> Listings { get; set; } = [];

    public virtual ICollection<Bookmark> Bookmarks { get; set; } = [];
    
    public class Specification : IValidatableObject
    {
        public Guid Id { get; init; }
    
        public required string Name { get; set; }
    
        public required string Value { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("Property cannot be empty",
                    [nameof(Name)]);
            
            if (string.IsNullOrEmpty(Value))
                yield return new ValidationResult("Property cannot be empty",
                    [nameof(Value)]);
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(Name)]);
        
        if (string.IsNullOrEmpty(Description))
            yield return new ValidationResult("Property cannot be empty",
                [nameof(Description)]);
    }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(b => b.Description)
            .HasMaxLength(800)
            .IsRequired();

        builder.OwnsMany(b => b.Specifications, b =>
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            b.Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();

            b.Property(s => s.Value)
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.Navigation(b => b.Listings)
            .AutoInclude();

        builder.HasOne(b => b.Category)
            .WithMany(b => b.Products)
            .IsRequired();
    }
}