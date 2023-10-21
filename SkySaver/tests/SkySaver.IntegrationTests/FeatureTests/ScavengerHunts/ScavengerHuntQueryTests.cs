namespace SkySaver.IntegrationTests.FeatureTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.Domain.ScavengerHunts.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class ScavengerHuntQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_scavengerhunt_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeScavengerHuntOne = new FakeScavengerHuntBuilder().Build();
        await testingServiceScope.InsertAsync(fakeScavengerHuntOne);

        // Act
        var query = new GetScavengerHunt.Query(fakeScavengerHuntOne.Id);
        var scavengerHunt = await testingServiceScope.SendAsync(query);

        // Assert
        scavengerHunt.HuntID.Should().Be(fakeScavengerHuntOne.HuntID);
        scavengerHunt.UserID.Should().Be(fakeScavengerHuntOne.UserID);
        scavengerHunt.PointsEarned.Should().Be(fakeScavengerHuntOne.PointsEarned);
        scavengerHunt.CompletionDate.Should().BeCloseTo(fakeScavengerHuntOne.CompletionDate, 1.Seconds());
    }

    [Fact]
    public async Task get_scavengerhunt_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetScavengerHunt.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}