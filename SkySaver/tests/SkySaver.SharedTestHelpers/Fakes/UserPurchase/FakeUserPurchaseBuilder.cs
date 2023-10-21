namespace SkySaver.SharedTestHelpers.Fakes.UserPurchase;

using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Models;

public class FakeUserPurchaseBuilder
{
    private UserPurchaseForCreation _creationData = new FakeUserPurchaseForCreation().Generate();

    public FakeUserPurchaseBuilder WithModel(UserPurchaseForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeUserPurchaseBuilder WithPurchaseID(int purchaseID)
    {
        _creationData.PurchaseID = purchaseID;
        return this;
    }
    
    public FakeUserPurchaseBuilder WithUserID(int userID)
    {
        _creationData.UserID = userID;
        return this;
    }
    
    public FakeUserPurchaseBuilder WithGoodID(int goodID)
    {
        _creationData.GoodID = goodID;
        return this;
    }
    
    public FakeUserPurchaseBuilder WithPurchaseDate(DateTime purchaseDate)
    {
        _creationData.PurchaseDate = purchaseDate;
        return this;
    }
    
    public UserPurchase Build()
    {
        var result = UserPurchase.Create(_creationData);
        return result;
    }
}