namespace SkySaver.Domain.ScavengerHunts;

using SharedKernel.Exceptions;
using SkySaver.Domain.ScavengerHunts.Models;
using SkySaver.Domain.ScavengerHunts.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class ScavengerHunt : BaseEntity
{
    public Guid UserID { get; private set; }

    public int PointsEarned { get; private set; }

    public DateTime CompletionDate { get; private set; }

    public SkySaver.Domain.Users.User User { get; set; }

    public static ScavengerHunt Create(ScavengerHuntForCreation scavengerHuntForCreation)
    {
        var newScavengerHunt = new ScavengerHunt();

        newScavengerHunt.UserID = scavengerHuntForCreation.UserID;
        newScavengerHunt.PointsEarned = scavengerHuntForCreation.PointsEarned;
        newScavengerHunt.CompletionDate = scavengerHuntForCreation.CompletionDate;

        newScavengerHunt.QueueDomainEvent(new ScavengerHuntCreated() { ScavengerHunt = newScavengerHunt });

        return newScavengerHunt;
    }

    public ScavengerHunt Update(ScavengerHuntForUpdate scavengerHuntForUpdate)
    {
        UserID = scavengerHuntForUpdate.UserID;
        PointsEarned = scavengerHuntForUpdate.PointsEarned;
        CompletionDate = scavengerHuntForUpdate.CompletionDate;

        QueueDomainEvent(new ScavengerHuntUpdated() { Id = Id });
        return this;
    }

    protected ScavengerHunt() { } // For EF + Mocking
}