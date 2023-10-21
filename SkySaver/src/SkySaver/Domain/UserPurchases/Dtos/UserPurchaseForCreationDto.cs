namespace SkySaver.Domain.UserPurchases.Dtos;

public sealed class UserPurchaseForCreationDto
{
    public int PurchaseID { get; set; }
    public int UserID { get; set; }
    public int GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}