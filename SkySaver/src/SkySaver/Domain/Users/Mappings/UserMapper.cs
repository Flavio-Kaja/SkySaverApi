namespace UserService.Domain.Users.Mappings;

using Riok.Mapperly.Abstractions;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Dtos;

[Mapper]
public static partial class UserMapper
{
    public static partial UserDto ToUserDto(this User user);
    public static partial IQueryable<UserDto> ToUserDtoQueryable(this IQueryable<User> user);
}