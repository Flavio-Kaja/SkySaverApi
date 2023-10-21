namespace SkySaver.SharedTestHelpers.Fakes.Streak;

using AutoBogus;
using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.Models;

public sealed class FakeStreakForCreation : AutoFaker<StreakForCreation>
{
    public FakeStreakForCreation()
    {
    }
}