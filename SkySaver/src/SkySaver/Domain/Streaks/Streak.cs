namespace SkySaver.Domain.Streaks;

using SharedKernel.Exceptions;
using SkySaver.Domain.Streaks.Models;
using SkySaver.Domain.Streaks.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class Streak : BaseEntity
{
    public int StreakID { get; private set; }

    public int UserID { get; private set; }

    public string StreakLevel { get; private set; }

    public bool IsActive { get; private set; }


    public static Streak Create(StreakForCreation streakForCreation)
    {
        var newStreak = new Streak();

        newStreak.StreakID = streakForCreation.StreakID;
        newStreak.UserID = streakForCreation.UserID;
        newStreak.StreakLevel = streakForCreation.StreakLevel;
        newStreak.IsActive = streakForCreation.IsActive;

        newStreak.QueueDomainEvent(new StreakCreated(){ Streak = newStreak });
        
        return newStreak;
    }

    public Streak Update(StreakForUpdate streakForUpdate)
    {
        StreakID = streakForUpdate.StreakID;
        UserID = streakForUpdate.UserID;
        StreakLevel = streakForUpdate.StreakLevel;
        IsActive = streakForUpdate.IsActive;

        QueueDomainEvent(new StreakUpdated(){ Id = Id });
        return this;
    }
    
    protected Streak() { } // For EF + Mocking
}