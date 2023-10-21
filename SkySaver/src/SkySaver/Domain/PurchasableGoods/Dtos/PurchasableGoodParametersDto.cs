namespace SkySaver.Domain.PurchasableGoods.Dtos;

using SharedKernel.Dtos;

public sealed class PurchasableGoodParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
