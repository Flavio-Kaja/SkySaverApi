namespace SkySaver.Domain.PurchasableGoods.Dtos;

public sealed class PurchasableGoodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PointsCost { get; set; }
    public string ImageUrl { get; private set; }
}
