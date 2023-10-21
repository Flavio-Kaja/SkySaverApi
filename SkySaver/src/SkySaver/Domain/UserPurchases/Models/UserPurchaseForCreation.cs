namespace SkySaver.Domain.UserPurchases.Models;

public sealed class UserPurchaseForCreation
{
    public int PurchaseID { get; set; }
    public Guid UserID{ get; set; }
    public int GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
