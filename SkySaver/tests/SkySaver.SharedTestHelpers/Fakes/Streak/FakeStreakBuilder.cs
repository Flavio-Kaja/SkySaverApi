namespace SkySaver.SharedTestHelpers.Fakes.Streak;

using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.Models;

public class FakeStreakBuilder
{
    private StreakForCreation _creationData = new FakeStreakForCreation().Generate();

    public FakeStreakBuilder WithModel(StreakForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeStreakBuilder WithStreakID(int streakID)
    {
        _creationData.StreakID = streakID;
        return this;
    }
    
    public FakeStreakBuilder WithUserID(int userID)
    {
        _creationData.UserID = userID;
        return this;
    }
    
    public FakeStreakBuilder WithStreakLevel(string streakLevel)
    {
        _creationData.StreakLevel = streakLevel;
        return this;
    }
    
    public FakeStreakBuilder WithIsActive(bool isActive)
    {
        _creationData.IsActive = isActive;
        return this;
    }
    
    public Streak Build()
    {
        var result = Streak.Create(_creationData);
        return result;
    }
}