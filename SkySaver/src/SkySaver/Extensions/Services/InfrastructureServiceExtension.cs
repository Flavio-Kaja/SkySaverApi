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
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            // this makes local migrations easier to manage. feel free to refactor if desired.
            connectionString = env.IsDevelopment() 
                ? "Host=localhost;Port=59904;Database=dev_skysaver;Username=postgres;Password=postgres"
                : throw new Exception("The database connection string is not set.");
        }

        services.AddDbContext<SkySaverDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(SkySaverDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());

        services.AddHostedService<MigrationHostedService<SkySaverDbContext>>();

        // Auth -- Do Not Delete
    }
}
