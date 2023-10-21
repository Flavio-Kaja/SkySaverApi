using SkySaver.Domain;

namespace SkySaver.Domain.Users.DomainEvents;

public class UserRolesUpdated : DomainEvent
{
    public Guid UserId;
}
