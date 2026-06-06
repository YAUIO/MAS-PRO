using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class DeliveryMethod
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required bool IsInStore { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; }
    
    public virtual ICollection<User.User.Seller> Sellers { get; set; }
}

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.IsInStore)
            .IsRequired();

        builder.HasMany(b => b.Sellers)
            .WithMany(b => b.SupportedDeliveryMethods);
    }
}