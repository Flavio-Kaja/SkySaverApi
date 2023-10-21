namespace SkySaver.Domain.Flights.Models;

public sealed class FlightForUpdate
{
    public int FlightID { get; set; }
    public int UserID { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public DateTime FlightDate { get; set; }
    public int PointsEarned { get; set; }
}