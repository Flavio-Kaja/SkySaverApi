namespace SkySaver.Databases.EntityConfigurations;

using SkySaver.Domain.UserPurchases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class UserPurchaseConfiguration : IEntityTypeConfiguration<UserPurchase>
{
    /// <summary>
    /// The database configuration for UserPurchases. 
    /// </summary>
    public void Configure(EntityTypeBuilder<UserPurchase> builder)
    {
        builder.HasOne(u => u.PurchasableGood).WithMany(u => u.UserPurchases).HasForeignKey(u => u.Id);
    }
}