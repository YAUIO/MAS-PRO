using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Purchase
{
    public Guid Id { get; init; }
    
    public required int Amount { get; set; }

    public required Listing Listing { get; set; }
    
    public required Order Order { get; set; }
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