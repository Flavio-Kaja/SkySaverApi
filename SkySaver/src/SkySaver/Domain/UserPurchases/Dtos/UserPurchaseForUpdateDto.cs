namespace SkySaver.Domain.UserPurchases.Dtos;

public sealed class UserPurchaseForUpdateDto
{
    public Guid UserID { get; set; }
    public Guid GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
