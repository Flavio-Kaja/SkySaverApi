namespace UserService.Domain.Users.Features;

using Mappings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Exceptions;
using SkySaver.Domain.Users.Dtos;
using SkySaver.Domain.Users.Services;
using SkySaver.Domain.Users;

public static class AddUser
{
    public sealed class Command : IRequest<UserDto>
    {
        public readonly PostUserDto UserToAdd;
        public readonly bool SkipPermissions;

        public Command(PostUserDto userToAdd, bool skipPermissions = false)
        {
            UserToAdd = userToAdd;
            SkipPermissions = skipPermissions;
        }
    }

    public sealed class Handler : IRequestHandler<Command, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public Handler(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (_userRepository.Query().Any(r => r.UserName == request.UserToAdd.Username))
                throw new ValidationException("Username", $"A user with username {request.UserToAdd.Username} already exists");
            var user = User.Create(request.UserToAdd);
            var result = await _userManager.CreateAsync(user, request.UserToAdd.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                var userAdded = await _userRepository.GetById(user.Id, cancellationToken: cancellationToken);
                return userAdded.ToUserDto();
            }
            else
                throw new Exception("User Creation Failed");
        }
    }
}
