namespace SkySaver.Domain.Flights.Models;

public sealed class FlightForCreation
{
    public Guid UserID { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public DateTime FlightDate { get; set; }
    public int PointsEarned { get; set; }
}
