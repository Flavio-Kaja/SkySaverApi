namespace SkySaver.IntegrationTests.FeatureTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using SkySaver.Domain.ScavengerHunts.Features;
using SharedKernel.Exceptions;

public class AddScavengerHuntCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_scavengerhunt_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeScavengerHuntOne = new FakeScavengerHuntForCreationDto().Generate();

        // Act
        var command = new AddScavengerHunt.Command(fakeScavengerHuntOne);
        var scavengerHuntReturned = await testingServiceScope.SendAsync(command);
        var scavengerHuntCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts
            .FirstOrDefaultAsync(s => s.Id == scavengerHuntReturned.Id));

        // Assert
        scavengerHuntReturned.HuntID.Should().Be(fakeScavengerHuntOne.HuntID);
        scavengerHuntReturned.UserID.Should().Be(fakeScavengerHuntOne.UserID);
        scavengerHuntReturned.PointsEarned.Should().Be(fakeScavengerHuntOne.PointsEarned);
        scavengerHuntReturned.CompletionDate.Should().BeCloseTo(fakeScavengerHuntOne.CompletionDate, 1.Seconds());

        scavengerHuntCreated.HuntID.Should().Be(fakeScavengerHuntOne.HuntID);
        scavengerHuntCreated.UserID.Should().Be(fakeScavengerHuntOne.UserID);
        scavengerHuntCreated.PointsEarned.Should().Be(fakeScavengerHuntOne.PointsEarned);
        scavengerHuntCreated.CompletionDate.Should().BeCloseTo(fakeScavengerHuntOne.CompletionDate, 1.Seconds());
    }
}