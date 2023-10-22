namespace SkySaver.Databases.EntityConfigurations;

using SkySaver.Domain.Flights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    /// <summary>
    /// The database configuration for Flights. 
    /// </summary>
    public void Configure(EntityTypeBuilder<Flight> builder)
    {

    }
}