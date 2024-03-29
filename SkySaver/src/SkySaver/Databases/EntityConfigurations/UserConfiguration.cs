namespace UserService.Databases.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkySaver.Databases.EntityConfigurations;
using SkySaver.Domain.Users;

public sealed class UserConfiguration : BaseEntityConfiguration<User>
{
    /// <summary>
    /// The db configuration for the Users. 
    /// </summary>
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(u => u.DailyGoal).IsRequired(true).HasDefaultValue(20);
        builder.Property(u => u.LastName).IsRequired(true).HasMaxLength(200);
        builder.Property(u => u.FirstName).IsRequired(true).HasMaxLength(200);
        builder.HasMany(u => u.Flight).WithOne(u => u.User).HasForeignKey(u => u.UserID);
        builder.HasMany(u => u.UserPurchase).WithOne(u => u.User).HasForeignKey(u => u.UserID);
    }
}