namespace SkySaver.UnitTests.Domain.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.Domain.ScavengerHunts;
using SkySaver.Domain.ScavengerHunts.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateScavengerHuntTests
{
    private readonly Faker _faker;

    public UpdateScavengerHuntTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_scavengerHunt()
    {
        // Arrange
        var fakeScavengerHunt = new FakeScavengerHuntBuilder().Build();
        var updatedScavengerHunt = new FakeScavengerHuntForUpdate().Generate();
        
        // Act
        fakeScavengerHunt.Update(updatedScavengerHunt);

        // Assert
        fakeScavengerHunt.HuntID.Should().Be(updatedScavengerHunt.HuntID);
        fakeScavengerHunt.UserID.Should().Be(updatedScavengerHunt.UserID);
        fakeScavengerHunt.PointsEarned.Should().Be(updatedScavengerHunt.PointsEarned);
        fakeScavengerHunt.CompletionDate.Should().BeCloseTo(updatedScavengerHunt.CompletionDate, 1.Seconds());
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeScavengerHunt = new FakeScavengerHuntBuilder().Build();
        var updatedScavengerHunt = new FakeScavengerHuntForUpdate().Generate();
        fakeScavengerHunt.DomainEvents.Clear();
        
        // Act
        fakeScavengerHunt.Update(updatedScavengerHunt);

        // Assert
        fakeScavengerHunt.DomainEvents.Count.Should().Be(1);
        fakeScavengerHunt.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ScavengerHuntUpdated));
    }
}