namespace SkySaver.Domain.PurchasableGoods.DomainEvents;

public sealed class PurchasableGoodCreated : DomainEvent
{
    public PurchasableGood PurchasableGood { get; set; } 
}
            