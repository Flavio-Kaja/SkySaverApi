namespace SkySaver.Domain.PurchasableGoods.Mappings;

using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.Domain.PurchasableGoods.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class PurchasableGoodMapper
{
    public static partial PurchasableGoodForCreation ToPurchasableGoodForCreation(this PurchasableGoodForCreationDto purchasableGoodForCreationDto);
    public static partial PurchasableGoodForUpdate ToPurchasableGoodForUpdate(this PurchasableGoodForUpdateDto purchasableGoodForUpdateDto);
    public static partial PurchasableGoodDto ToPurchasableGoodDto(this PurchasableGood purchasableGood);
    public static partial IQueryable<PurchasableGoodDto> ToPurchasableGoodDtoQueryable(this IQueryable<PurchasableGood> purchasableGood);
}