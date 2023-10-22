using System;

namespace SkySaver.Domain.UserPurchases.Models;

public sealed class UserPurchaseForUpdate
{
    public Guid UserID { get; set; }
    public Guid GoodID { get; set; }
    public DateTime PurchaseDate { get; set; }
}
