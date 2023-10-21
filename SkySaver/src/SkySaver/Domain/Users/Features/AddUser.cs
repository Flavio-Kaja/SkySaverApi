namespace SkySaver.Domain.Users.Features;

using SkySaver.Domain.Users.Services;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Dtos;
using SkySaver.Domain.Users.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class AddUser
{
    public sealed class Command : IRequest<UserDto>
    {
        public readonly UserForCreationDto UserToAdd;

        public Command(UserForCreationDto userToAdd)
        {
            UserToAdd = userToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userToAdd = request.UserToAdd.ToUserForCreation();
            var user = User.Create(userToAdd);

            await _userRepository.Add(user, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return user.ToUserDto();
        }
    }
}