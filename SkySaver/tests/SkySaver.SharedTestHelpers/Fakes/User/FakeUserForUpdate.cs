namespace SkySaver.SharedTestHelpers.Fakes.User;

using AutoBogus;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Models;

public sealed class FakeUserForUpdate : AutoFaker<UserForUpdate>
{
    public FakeUserForUpdate()
    {
    }
}