namespace SkySaver.Extensions.Services;

using SkySaver.Middleware;
using SkySaver.Services;
using Configurations;
using System.Text.Json.Serialization;
using Serilog;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Resources;
using Sieve.Services;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using SkySaver.Domain.Users;
using SkySaver.Domain.Roles;
using SkySaver.Databases;
using Microsoft.AspNetCore.Identity;
using SkySaver.Extensions.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using SkySaver.Domain.RolePermissions.Dtos;
using SkySaver.Authentication.Models;
using SkySaver.Domain.Users.Dtos;

public static class WebAppServiceConfiguration
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddSingleton(Log.Logger);
        builder.Services.AddProblemDetails(ProblemDetailsConfigurationExtension.ConfigureProblemDetails)
            .AddProblemDetailsConventions();


        builder.Services.AddCorsService("SkySaverCorsPolicy", builder.Environment);
        builder.OpenTelemetryRegistration(builder.Configuration, "SkySaver");
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        builder.Services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<SkySaverDbContext>()
               .AddDefaultTokenProviders();
        builder.Services.AddInfrastructure(builder.Environment, builder.Configuration);
        //Added authentication
        builder.Services.AddAppAuthentication(builder.Environment, builder.Configuration);
        builder.Services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddScoped<SieveProcessor>();
        builder.Services.AddValidatorsFromAssemblyContaining<PostRolePermissionDto>();

        builder.Services.AddScoped<IValidator<PostRolePermissionDto>, PostRolePermissionDtoValidator>();
        builder.Services.AddScoped<IValidator<UserLoginModel>, UserLoginValidator>();
        builder.Services.AddScoped<IValidator<PostUserDto>, PostUserDtoValidator>();

        builder.Services.AddMvc().AddFluentValidation(op => op.AutomaticValidationEnabled = false);
        builder.Services.AddBoundaryServices(Assembly.GetExecutingAssembly());


        builder.Services.AddHealthChecks();
        builder.Services.AddSwaggerExtension(builder.Configuration);

    }

    /// <summary>
    /// Registers all services in the assembly of the given interface.
    /// </summary>
    private static void AddBoundaryServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (!assemblies.Any())
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");

        foreach (var assembly in assemblies)
        {
            var rules = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(nameof(ISkySaverScopedService)) == typeof(ISkySaverScopedService));

            foreach (var rule in rules)
            {
                foreach (var @interface in rule.GetInterfaces())
                {
                    services.Add(new ServiceDescriptor(@interface, rule, ServiceLifetime.Scoped));
                }
            }
        }
    }
}