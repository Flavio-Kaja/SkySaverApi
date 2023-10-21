namespace SkySaver.Domain.PurchasableGoods.DomainEvents;

public sealed class PurchasableGoodUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            