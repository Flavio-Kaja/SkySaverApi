﻿using HeimGuard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using SkySaver.Domain;
using SkySaver.Services;

namespace SkySaver.Extensions.Services;

public static class AuthenticationExtension
{

    public static void AddAppAuthentication(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        string encryptionKey = configuration["Auth:ClientSecret"];
        byte[] key = Encoding.ASCII.GetBytes(encryptionKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = !env.IsDevelopment();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["Auth:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Permissions.CanUpdate, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanUpdate));
            options.AddPolicy(Permissions.CanReadPreferences, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanReadPreferences));
            options.AddPolicy(Permissions.CanDeleteUsers, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanDeleteUsers));
            options.AddPolicy(Permissions.CanUpdateUsers, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanUpdateUsers));
            options.AddPolicy(Permissions.CanAddUsers, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanAddUsers));
            options.AddPolicy(Permissions.CanReadUsers, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanReadUsers));
            options.AddPolicy(Permissions.CanDeleteRolePermissions, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanDeleteRolePermissions));
            options.AddPolicy(Permissions.CanUpdateRolePermissions, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanUpdateRolePermissions));
            options.AddPolicy(Permissions.CanAddRolePermissions, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanAddRolePermissions));
            options.AddPolicy(Permissions.CanReadRolePermissions, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanReadRolePermissions));
            options.AddPolicy(Permissions.CanRemoveUserRoles, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanRemoveUserRoles));
            options.AddPolicy(Permissions.CanAddUserRoles, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanAddUserRoles));
            options.AddPolicy(Permissions.CanGetRoles, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanGetRoles));
            options.AddPolicy(Permissions.CanGetPermissions, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanGetPermissions));
            options.AddPolicy(Permissions.CanCreateProduct, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanCreateProduct));
            options.AddPolicy(Permissions.CanReadProduct, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanReadProduct));
            options.AddPolicy(Permissions.CanUpdateProduct, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanUpdateProduct));
            options.AddPolicy(Permissions.CanDeleteProduct, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanDeleteProduct));
            options.AddPolicy(Permissions.CanCreateTransportation, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanCreateTransportation));
            options.AddPolicy(Permissions.CanReadTransportation, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanReadTransportation));
            options.AddPolicy(Permissions.CanUpdateTransportation, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanUpdateTransportation));
            options.AddPolicy(Permissions.CanDeleteTransportation, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanDeleteTransportation));
            options.AddPolicy(Permissions.CanViewMonthlyRouteDistance, policy => policy.RequireClaim(ClaimTypes.AuthorizationDecision, Permissions.CanViewMonthlyRouteDistance));

        });

        services.AddHeimGuard<UserPolicyHandler>()
            .MapAuthorizationPolicies()
            .AutomaticallyCheckPermissions();
    }

}
