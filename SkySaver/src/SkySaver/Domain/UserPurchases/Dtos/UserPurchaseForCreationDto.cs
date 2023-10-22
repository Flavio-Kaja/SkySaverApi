namespace SkySaver.Domain.UserPurchases.Dtos;

public sealed class UserPurchaseForCreationDto
{
    public Guid UserID { get; set; }
    public Guid GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
