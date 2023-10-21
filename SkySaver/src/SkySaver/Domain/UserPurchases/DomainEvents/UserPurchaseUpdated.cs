namespace SkySaver.Domain.UserPurchases.DomainEvents;

public sealed class UserPurchaseUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            