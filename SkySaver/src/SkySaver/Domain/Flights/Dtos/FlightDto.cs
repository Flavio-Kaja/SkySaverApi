namespace SkySaver.Domain.Flights.Dtos;

public sealed class FlightDto
{
    public Guid Id { get; set; }
    public int FlightID { get; set; }
    public Guid UserID { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public DateTime FlightDate { get; set; }
    public int PointsEarned { get; set; }
}
