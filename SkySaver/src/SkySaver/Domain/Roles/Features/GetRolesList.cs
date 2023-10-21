using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SkySaver.Domain.RolePermissions.Dtos;
using SkySaver.Domain.Roles.Dtos;
using SkySaver.Domain.Roles.Mappings;

namespace SkySaver.Domain.Roles.Features;
public static class GetRolesList
{
    public sealed class Query : IRequest<IList<RoleDto>>
    {

        public Query()
        {
        }
    }

    public sealed class Handler : IRequestHandler<Query, IList<RoleDto>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly RoleManager<Role> _roleManager;

        public Handler(ILogger<Handler> logger, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<IList<RoleDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving roles list");
            var roles = _roleManager.Roles.AsQueryable().ToRoleDtoQueryable().ToList();
            return roles;
        }
    }
}
