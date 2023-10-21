namespace SkySaver.SharedTestHelpers.Fakes.User;

using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Models;

public class FakeUserBuilder
{
    private UserForCreation _creationData = new FakeUserForCreation().Generate();

    public FakeUserBuilder WithModel(UserForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeUserBuilder WithUserID(int userID)
    {
        _creationData.UserID = userID;
        return this;
    }
    
    public FakeUserBuilder WithEmail(string email)
    {
        _creationData.Email = email;
        return this;
    }
    
    public FakeUserBuilder WithFirstName(string firstName)
    {
        _creationData.FirstName = firstName;
        return this;
    }
    
    public FakeUserBuilder WithLastName(string lastName)
    {
        _creationData.LastName = lastName;
        return this;
    }
    
    public FakeUserBuilder WithSkyPoints(int skyPoints)
    {
        _creationData.SkyPoints = skyPoints;
        return this;
    }
    
    public FakeUserBuilder WithCurrentStreak(int currentStreak)
    {
        _creationData.CurrentStreak = currentStreak;
        return this;
    }
    
    public FakeUserBuilder WithStreakLevel(string streakLevel)
    {
        _creationData.StreakLevel = streakLevel;
        return this;
    }
    
    public User Build()
    {
        var result = User.Create(_creationData);
        return result;
    }
}