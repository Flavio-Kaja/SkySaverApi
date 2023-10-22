namespace SkySaver.Databases.EntityConfigurations;

using SkySaver.Domain.PurchasableGoods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class PurchasableGoodConfiguration : IEntityTypeConfiguration<PurchasableGood>
{
    /// <summary>
    /// The database configuration for PurchasableGoods. 
    /// </summary>
    public void Configure(EntityTypeBuilder<PurchasableGood> builder)
    {
    }
}