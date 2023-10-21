

using Riok.Mapperly.Abstractions;
using SkySaver.Domain.Roles.Dtos;
using SkySaver.Domain.Users;

namespace SkySaver.Domain.Roles.Mappings
{
    /// <summary>
    /// Role mapping class
    /// </summary>
    [Mapper]
    public static partial class RoleMapper
    {
        public static partial RoleDto ToRoleDto(this Role role);
        public static partial IQueryable<RoleDto> ToRoleDtoQueryable(this IQueryable<Role> user);
    }
}
