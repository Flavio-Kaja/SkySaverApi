using SkySaver.Domain;

namespace SkySaver.Domain.RolePermissions.DomainEvents;

public sealed class RolePermissionCreated : DomainEvent
{
    public RolePermission RolePermission { get; set; }
}
