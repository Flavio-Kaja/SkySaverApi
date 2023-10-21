namespace SkySaver.Extensions.Services;

using SkySaver.Databases;
using SkySaver.Resources;
using SkySaver.Services;
using Configurations;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        // DbContext -- Do Not Delete
        var connectionString = configuration.GetConnectionStringOptions().SkySaver;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("The database connection string is not set.");
        }

        services.AddDbContext<SkySaverDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(SkySaverDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());

        services.AddHostedService<MigrationHostedService<SkySaverDbContext>>();
    }
}
