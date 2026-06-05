using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Listing
{
    public Guid Id { get; init; }
    
    public required double Price { get; set; }
    
    public required Product Product { get; set; }
    
    public required User.User.Seller Seller { get; set; }
}

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Price)
            .IsRequired();

        builder.HasOne(b => b.Product)
            .WithMany(b => b.Listings)
            .IsRequired();

        builder.HasOne(b => b.Seller)
            .WithMany(b => b.Listings);
    }
}