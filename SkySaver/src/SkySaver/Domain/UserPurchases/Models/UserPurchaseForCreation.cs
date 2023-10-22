namespace SkySaver.Domain.UserPurchases.Models;

public sealed class UserPurchaseForCreation
{
    public Guid UserID { get; set; }
    public Guid GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
