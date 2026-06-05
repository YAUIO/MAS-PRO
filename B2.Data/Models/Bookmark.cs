using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Models;

public class Bookmark
{
    public Guid Id { get; init; }
    
    public required string Note { get; set; }
    
    public required Product Product { get; set; }
    
    public required User.User.Customer Customer { get; set; }
}

public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Note)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.HasOne(b => b.Product);
        builder.Property(b => b.Product)
            .IsRequired();
        
        builder.HasOne(b => b.Customer)
            .WithMany(c => c.Bookmarks)
            .IsRequired();
    }
}