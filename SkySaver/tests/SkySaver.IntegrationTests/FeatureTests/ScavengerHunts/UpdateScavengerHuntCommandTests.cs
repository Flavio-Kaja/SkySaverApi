namespace SkySaver.IntegrationTests.FeatureTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.Domain.ScavengerHunts.Dtos;
using SharedKernel.Exceptions;
using SkySaver.Domain.ScavengerHunts.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateScavengerHuntCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_scavengerhunt_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeScavengerHuntOne = new FakeScavengerHuntBuilder().Build();
        var updatedScavengerHuntDto = new FakeScavengerHuntForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeScavengerHuntOne);

        var scavengerHunt = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts
            .FirstOrDefaultAsync(s => s.Id == fakeScavengerHuntOne.Id));

        // Act
        var command = new UpdateScavengerHunt.Command(scavengerHunt.Id, updatedScavengerHuntDto);
        await testingServiceScope.SendAsync(command);
        var updatedScavengerHunt = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts.FirstOrDefaultAsync(s => s.Id == scavengerHunt.Id));

        // Assert
        updatedScavengerHunt.HuntID.Should().Be(updatedScavengerHuntDto.HuntID);
        updatedScavengerHunt.UserID.Should().Be(updatedScavengerHuntDto.UserID);
        updatedScavengerHunt.PointsEarned.Should().Be(updatedScavengerHuntDto.PointsEarned);
        updatedScavengerHunt.CompletionDate.Should().BeCloseTo(updatedScavengerHuntDto.CompletionDate, 1.Seconds());
    }
}