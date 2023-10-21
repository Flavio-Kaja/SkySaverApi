namespace SkySaver.SharedTestHelpers.Fakes.UserPurchase;

using AutoBogus;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Dtos;

public sealed class FakeUserPurchaseForUpdateDto : AutoFaker<UserPurchaseForUpdateDto>
{
    public FakeUserPurchaseForUpdateDto()
    {
    }
}