namespace SkySaver.Domain.UserPurchases.Models;

public sealed class UserPurchaseForUpdate
{
    public int PurchaseID { get; set; }
    public int UserID { get; set; }
    public int GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
