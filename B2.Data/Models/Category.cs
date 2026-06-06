using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Category
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public virtual ICollection<Product> Products { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(s => s.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}