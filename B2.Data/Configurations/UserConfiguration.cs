using B2.Data.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(b => b.Id);

        builder.ComplexProperty(b => b.CustomerObj);
        builder.ComplexProperty(b => b.SellerObj);

        builder.Ignore(b => b.IsCustomer);
        builder.Ignore(b => b.IsSeller);
    }
}