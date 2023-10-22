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
    public Guid UserID { get; private set; }

    public string StreakLevel { get; private set; }

    public bool IsActive { get; private set; }
    public decimal Counter { get; set; }
    public string Decimal { get; set; }

    public Users.User User { get; set; }

    public static Streak Create(Guid userId)
    {
        var newStreak = new Streak();

        newStreak.UserID = userId;
        newStreak.StreakLevel = StreakLevelEnum.None.ToString();
        newStreak.IsActive = true;

        newStreak.QueueDomainEvent(new StreakCreated() { Streak = newStreak });

        return newStreak;
    }

    public static Streak Create(StreakForCreation streakForCreation)
    {
        var newStreak = new Streak();

        newStreak.UserID = streakForCreation.UserID;
        newStreak.StreakLevel = streakForCreation.StreakLevel;
        newStreak.IsActive = streakForCreation.IsActive;

        newStreak.QueueDomainEvent(new StreakCreated() { Streak = newStreak });

        return newStreak;
    }

    public Streak Update(StreakForUpdate streakForUpdate)
    {
        UserID = streakForUpdate.UserID;
        StreakLevel = streakForUpdate.StreakLevel;
        IsActive = streakForUpdate.IsActive;

        QueueDomainEvent(new StreakUpdated() { Id = Id });
        return this;
    }

    protected Streak() { } // For EF + Mocking
}
enum StreakLevelEnum
{
    None = 0,
    Bronze = 1,
    Silver = 2,
    Gold = 3,
    Platinum = 4
}