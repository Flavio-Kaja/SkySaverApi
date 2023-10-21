namespace SkySaver.SharedTestHelpers.Fakes.User;

using AutoBogus;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Dtos;

public sealed class FakeUserForUpdateDto : AutoFaker<UserForUpdateDto>
{
    public FakeUserForUpdateDto()
    {
    }
}