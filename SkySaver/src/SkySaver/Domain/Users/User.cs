namespace SkySaver.Domain.Users;

using SharedKernel.Exceptions;
using SkySaver.Domain.Users.Models;
using SkySaver.Domain.Users.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class User : BaseEntity
{
    public int UserID { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public int SkyPoints { get; private set; }

    public int CurrentStreak { get; private set; }

    public string StreakLevel { get; private set; }


    public static User Create(UserForCreation userForCreation)
    {
        var newUser = new User();

        newUser.UserID = userForCreation.UserID;
        newUser.Email = userForCreation.Email;
        newUser.FirstName = userForCreation.FirstName;
        newUser.LastName = userForCreation.LastName;
        newUser.SkyPoints = userForCreation.SkyPoints;
        newUser.CurrentStreak = userForCreation.CurrentStreak;
        newUser.StreakLevel = userForCreation.StreakLevel;

        newUser.QueueDomainEvent(new UserCreated(){ User = newUser });
        
        return newUser;
    }

    public User Update(UserForUpdate userForUpdate)
    {
        UserID = userForUpdate.UserID;
        Email = userForUpdate.Email;
        FirstName = userForUpdate.FirstName;
        LastName = userForUpdate.LastName;
        SkyPoints = userForUpdate.SkyPoints;
        CurrentStreak = userForUpdate.CurrentStreak;
        StreakLevel = userForUpdate.StreakLevel;

        QueueDomainEvent(new UserUpdated(){ Id = Id });
        return this;
    }
    
    protected User() { } // For EF + Mocking
}