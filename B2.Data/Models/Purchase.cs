using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Purchase : IValidatableObject
{
    public Guid Id { get; init; }
    
    public required int Amount { get; set; }

    public required Listing Listing { get; set; }
    
    public Order Order { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Amount <= 0)
            yield return new ValidationResult("Property cannot be <= 0",
            [nameof(Amount)]);
    }
}

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Amount)
            .IsRequired();
    }
}