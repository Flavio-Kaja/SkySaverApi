namespace SkySaver.Domain.RolePermissions.Features;

using SkySaver.Domain.RolePermissions;
using SkySaver.Domain.RolePermissions.Dtos;
using SkySaver.Domain;
using HeimGuard;
using Mappings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SkySaver.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SharedKernel.Exceptions;

public static class AddRolePermission
{
    public sealed class Command : IRequest<RolePermissionDto>
    {
        public readonly PostRolePermissionDto RolePermissionToAdd;

        public Command(PostRolePermissionDto rolePermissionToAdd)
        {
            RolePermissionToAdd = rolePermissionToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, RolePermissionDto>
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Role> _roleManager;
        private readonly ILogger<Handler> _logger;

        public Handler(Microsoft.AspNetCore.Identity.RoleManager<Role> roleManager, ILogger<Handler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<RolePermissionDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var rolePermission = RolePermission.Create(request.RolePermissionToAdd);
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == request.RolePermissionToAdd.Role);
            if (role is null)
            {
                throw new NotFoundException($"Role with name {request.RolePermissionToAdd.Role} not found");
            }

            var claimResult = await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.AuthorizationDecision, request.RolePermissionToAdd.Permission));
            if (!claimResult.Succeeded)
            {

                _logger.LogError("Adding role permissions failed: role object {@user}, identity result: {@identityResult}", role, claimResult);
                throw new NotFoundException(string.Join(", ", claimResult.Errors));
            }
            return rolePermission.ToRolePermissionDto();
        }
    }
}