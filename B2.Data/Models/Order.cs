using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Order : IValidatableObject
{
    public Guid Id { get; init; }
    
    public required DateTime PlacedAt { get; init; }
    
    public required OrderStatus Status { get; set; }
    
    public required User.User.Customer Customer { get; set; }

    public required DeliveryMethod DeliveryMethod { get; set; }
    
    public Location? Location { get; set; }
    
    public virtual ICollection<Location> Pickups { get; set; }
    
    public virtual ICollection<Purchase> Purchases { get; set; } = [];
    
    public void MarkAsAwaitingPickup()
    {
        Status = OrderStatus.AwaitingPickup;
    }

    public void MarkAsDelivered()
    {
        Status = OrderStatus.Delivered;
    }
    
    public enum OrderStatus
    {
        Created, AwaitingPickup, Delivered
    }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Purchases.Count == 0)
            yield return new ValidationResult("Collection cannot be empty",
                [nameof(Purchases)]);
        
        if (PlacedAt > DateTime.Now)
            yield return new ValidationResult("Date cannot be in the future",
                [nameof(PlacedAt)]);
        
        
        if (Location is null && Pickups is null)
        {
            yield return new ValidationResult("One of two is required", [nameof(Location), nameof(Pickups)]);
        }
        
        if (Location is null && !DeliveryMethod.IsInStore)
        {
            yield return new ValidationResult("Required for delivery method", [nameof(Location)]);
        }
        
        if ((Pickups is null || Pickups.Count == 0) && DeliveryMethod.IsInStore)
        {
            yield return new ValidationResult("Required for delivery method", [nameof(Pickups)]);
        }
    }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.PlacedAt)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired();

        builder.HasOne(b => b.Customer)
            .WithMany(b => b.Orders)
            .IsRequired();

        builder.HasOne(b => b.Location)
            .WithMany(b => b.Orders);
        
        builder.HasMany(b => b.Pickups)
            .WithMany();

        builder.HasOne(b => b.DeliveryMethod)
            .WithMany(b => b.Orders)
            .IsRequired();

        builder.HasMany(b => b.Purchases)
            .WithOne(b => b.Order)
            .IsRequired();
        
        builder.Navigation(b => b.Purchases)
            .AutoInclude();
    }
}