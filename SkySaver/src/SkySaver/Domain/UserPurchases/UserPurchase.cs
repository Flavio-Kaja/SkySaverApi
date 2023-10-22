namespace SkySaver.Domain.UserPurchases;

using SharedKernel.Exceptions;
using SkySaver.Domain.UserPurchases.Models;
using SkySaver.Domain.UserPurchases.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class UserPurchase : BaseEntity
{

    public Guid UserID { get; private set; }

    public Guid GoodID { get; private set; }

    public DateTime PurchaseDate { get; private set; }

    public SkySaver.Domain.Users.User User { get; set; }


    public PurchasableGoods.PurchasableGood PurchasableGood { get; set; }


    public static UserPurchase Create(UserPurchaseForCreation userPurchaseForCreation)
    {
        var newUserPurchase = new UserPurchase();

        newUserPurchase.UserID = userPurchaseForCreation.UserID;
        newUserPurchase.GoodID = userPurchaseForCreation.GoodID;
        newUserPurchase.PurchaseDate = userPurchaseForCreation.PurchaseDate;

        newUserPurchase.QueueDomainEvent(new UserPurchaseCreated() { UserPurchase = newUserPurchase });

        return newUserPurchase;
    }

    public UserPurchase Update(UserPurchaseForUpdate userPurchaseForUpdate)
    {
        UserID = userPurchaseForUpdate.UserID;
        PurchaseDate = userPurchaseForUpdate.PurchaseDate;

        QueueDomainEvent(new UserPurchaseUpdated() { Id = Id });
        return this;
    }

    protected UserPurchase() { } // For EF + Mocking
}