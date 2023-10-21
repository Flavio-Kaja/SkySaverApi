namespace SkySaver.IntegrationTests.FeatureTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.Domain.ScavengerHunts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteScavengerHuntCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_scavengerhunt_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeScavengerHuntOne = new FakeScavengerHuntBuilder().Build();
        await testingServiceScope.InsertAsync(fakeScavengerHuntOne);
        var scavengerHunt = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts
            .FirstOrDefaultAsync(s => s.Id == fakeScavengerHuntOne.Id));

        // Act
        var command = new DeleteScavengerHunt.Command(scavengerHunt.Id);
        await testingServiceScope.SendAsync(command);
        var scavengerHuntResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts.CountAsync(s => s.Id == scavengerHunt.Id));

        // Assert
        scavengerHuntResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_scavengerhunt_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteScavengerHunt.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_scavengerhunt_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeScavengerHuntOne = new FakeScavengerHuntBuilder().Build();
        await testingServiceScope.InsertAsync(fakeScavengerHuntOne);
        var scavengerHunt = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts
            .FirstOrDefaultAsync(s => s.Id == fakeScavengerHuntOne.Id));

        // Act
        var command = new DeleteScavengerHunt.Command(scavengerHunt.Id);
        await testingServiceScope.SendAsync(command);
        var deletedScavengerHunt = await testingServiceScope.ExecuteDbContextAsync(db => db.ScavengerHunts
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == scavengerHunt.Id));

        // Assert
        deletedScavengerHunt?.IsDeleted.Should().BeTrue();
    }
}