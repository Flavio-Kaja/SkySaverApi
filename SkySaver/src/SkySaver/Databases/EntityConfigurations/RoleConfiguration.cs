using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkySaver.Domain.Roles;
using SkySaver.Domain.Users;

namespace SkySaver.Databases.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// The db configuration for the Users. 
        /// </summary>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
        }
    }
}