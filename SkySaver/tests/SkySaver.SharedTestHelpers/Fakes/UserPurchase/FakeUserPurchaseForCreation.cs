namespace SkySaver.SharedTestHelpers.Fakes.UserPurchase;

using AutoBogus;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Models;

public sealed class FakeUserPurchaseForCreation : AutoFaker<UserPurchaseForCreation>
{
    public FakeUserPurchaseForCreation()
    {
    }
}