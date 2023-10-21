namespace SkySaver.Domain.UserPurchases.DomainEvents;

public sealed class UserPurchaseCreated : DomainEvent
{
    public UserPurchase UserPurchase { get; set; } 
}
            