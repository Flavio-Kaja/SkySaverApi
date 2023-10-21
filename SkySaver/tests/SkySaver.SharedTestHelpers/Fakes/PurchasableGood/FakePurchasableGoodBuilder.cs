namespace SkySaver.SharedTestHelpers.Fakes.PurchasableGood;

using SkySaver.Domain.PurchasableGoods;
using SkySaver.Domain.PurchasableGoods.Models;

public class FakePurchasableGoodBuilder
{
    private PurchasableGoodForCreation _creationData = new FakePurchasableGoodForCreation().Generate();

    public FakePurchasableGoodBuilder WithModel(PurchasableGoodForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakePurchasableGoodBuilder WithGoodID(int goodID)
    {
        _creationData.GoodID = goodID;
        return this;
    }
    
    public FakePurchasableGoodBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public FakePurchasableGoodBuilder WithDescription(string description)
    {
        _creationData.Description = description;
        return this;
    }
    
    public FakePurchasableGoodBuilder WithPointsCost(int pointsCost)
    {
        _creationData.PointsCost = pointsCost;
        return this;
    }
    
    public PurchasableGood Build()
    {
        var result = PurchasableGood.Create(_creationData);
        return result;
    }
}