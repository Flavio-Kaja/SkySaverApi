namespace SkySaver.Domain.UserPurchases.Dtos;

public sealed class UserPurchaseForUpdateDto
{
    public int PurchaseID { get; set; }
    public Guid UserID{ get; set; }
    public int GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
