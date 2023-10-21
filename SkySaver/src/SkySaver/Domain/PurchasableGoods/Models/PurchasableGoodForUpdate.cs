namespace SkySaver.Domain.PurchasableGoods.Models;

public sealed class PurchasableGoodForUpdate
{
    public int GoodID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PointsCost { get; set; }
}
