namespace SkySaver.Domain.Users.Features;

using SkySaver.Domain.Users.Services;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Dtos;
using HeimGuard;
using MediatR;
using Roles;
using Microsoft.AspNetCore.Identity;
using SkySaver.Services;
using SharedKernel.Exceptions;

public static class AddUserRole
{
    public sealed class Command : IRequest
    {
        public readonly Guid UserId;
        public readonly string Role;
        public readonly bool SkipPermissions;

        public Command(Guid userId, string role, bool skipPermissions = false)
        {
            UserId = userId;
            Role = role;
            SkipPermissions = skipPermissions;
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IHeimGuardClient heimGuard, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _heimGuard = heimGuard;
            _userManager = userManager;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            if (!request.SkipPermissions)
                await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanAddUserRoles);

            var user = await _userRepository.GetById(request.UserId, true, cancellationToken);
            await _userManager.AddToRoleAsync(user, request.Role);
            var roleToAdd = user.AddRole(new Role(request.Role));

            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}