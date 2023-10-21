namespace SkySaver.UnitTests.Domain.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.Domain.ScavengerHunts;
using SkySaver.Domain.ScavengerHunts.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateScavengerHuntTests
{
    private readonly Faker _faker;

    public CreateScavengerHuntTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_scavengerHunt()
    {
        // Arrange
        var scavengerHuntToCreate = new FakeScavengerHuntForCreation().Generate();
        
        // Act
        var fakeScavengerHunt = ScavengerHunt.Create(scavengerHuntToCreate);

        // Assert
        fakeScavengerHunt.HuntID.Should().Be(scavengerHuntToCreate.HuntID);
        fakeScavengerHunt.UserID.Should().Be(scavengerHuntToCreate.UserID);
        fakeScavengerHunt.PointsEarned.Should().Be(scavengerHuntToCreate.PointsEarned);
        fakeScavengerHunt.CompletionDate.Should().BeCloseTo(scavengerHuntToCreate.CompletionDate, 1.Seconds());
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var scavengerHuntToCreate = new FakeScavengerHuntForCreation().Generate();
        
        // Act
        var fakeScavengerHunt = ScavengerHunt.Create(scavengerHuntToCreate);

        // Assert
        fakeScavengerHunt.DomainEvents.Count.Should().Be(1);
        fakeScavengerHunt.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ScavengerHuntCreated));
    }
}