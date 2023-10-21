namespace SkySaver.SharedTestHelpers.Fakes.UserPurchase;

using AutoBogus;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Dtos;

public sealed class FakeUserPurchaseForCreationDto : AutoFaker<UserPurchaseForCreationDto>
{
    public FakeUserPurchaseForCreationDto()
    {
    }
}