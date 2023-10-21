namespace SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;

using SkySaver.Domain.ScavengerHunts;
using SkySaver.Domain.ScavengerHunts.Models;

public class FakeScavengerHuntBuilder
{
    private ScavengerHuntForCreation _creationData = new FakeScavengerHuntForCreation().Generate();

    public FakeScavengerHuntBuilder WithModel(ScavengerHuntForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeScavengerHuntBuilder WithHuntID(int huntID)
    {
        _creationData.HuntID = huntID;
        return this;
    }
    
    public FakeScavengerHuntBuilder WithUserID(int userID)
    {
        _creationData.UserID = userID;
        return this;
    }
    
    public FakeScavengerHuntBuilder WithPointsEarned(int pointsEarned)
    {
        _creationData.PointsEarned = pointsEarned;
        return this;
    }
    
    public FakeScavengerHuntBuilder WithCompletionDate(DateTime completionDate)
    {
        _creationData.CompletionDate = completionDate;
        return this;
    }
    
    public ScavengerHunt Build()
    {
        var result = ScavengerHunt.Create(_creationData);
        return result;
    }
}