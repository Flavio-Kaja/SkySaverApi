namespace SkySaver.Domain.UserPurchases.Mappings;

using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Domain.UserPurchases.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class UserPurchaseMapper
{
    public static partial UserPurchaseForCreation ToUserPurchaseForCreation(this UserPurchaseForCreationDto userPurchaseForCreationDto);
    public static partial UserPurchaseForUpdate ToUserPurchaseForUpdate(this UserPurchaseForUpdateDto userPurchaseForUpdateDto);
    public static partial UserPurchaseDto ToUserPurchaseDto(this UserPurchase userPurchase);
    public static partial IQueryable<UserPurchaseDto> ToUserPurchaseDtoQueryable(this IQueryable<UserPurchase> userPurchase);
}