namespace SkySaver.Domain.UserPurchases.Dtos;

public sealed class UserPurchaseDto
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }
    public Guid GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
