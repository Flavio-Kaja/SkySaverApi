namespace SkySaver.Domain.PurchasableGoods.Dtos;

public sealed class PurchasableGoodForCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int PointsCost { get; set; }
}
