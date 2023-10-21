namespace SkySaver.Domain.PurchasableGoods;

using SharedKernel.Exceptions;
using SkySaver.Domain.PurchasableGoods.Models;
using SkySaver.Domain.PurchasableGoods.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class PurchasableGood : BaseEntity
{
    public int GoodID { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public int PointsCost { get; private set; }


    public static PurchasableGood Create(PurchasableGoodForCreation purchasableGoodForCreation)
    {
        var newPurchasableGood = new PurchasableGood();

        newPurchasableGood.GoodID = purchasableGoodForCreation.GoodID;
        newPurchasableGood.Name = purchasableGoodForCreation.Name;
        newPurchasableGood.Description = purchasableGoodForCreation.Description;
        newPurchasableGood.PointsCost = purchasableGoodForCreation.PointsCost;

        newPurchasableGood.QueueDomainEvent(new PurchasableGoodCreated(){ PurchasableGood = newPurchasableGood });
        
        return newPurchasableGood;
    }

    public PurchasableGood Update(PurchasableGoodForUpdate purchasableGoodForUpdate)
    {
        GoodID = purchasableGoodForUpdate.GoodID;
        Name = purchasableGoodForUpdate.Name;
        Description = purchasableGoodForUpdate.Description;
        PointsCost = purchasableGoodForUpdate.PointsCost;

        QueueDomainEvent(new PurchasableGoodUpdated(){ Id = Id });
        return this;
    }
    
    protected PurchasableGood() { } // For EF + Mocking
}