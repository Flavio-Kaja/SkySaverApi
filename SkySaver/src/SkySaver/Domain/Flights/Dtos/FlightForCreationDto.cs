namespace SkySaver.Domain.Flights.Dtos;

public sealed class FlightForCreationDto
{
    public int FlightID { get; set; }
    public int UserID { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public DateTime FlightDate { get; set; }
    public int PointsEarned { get; set; }
}
