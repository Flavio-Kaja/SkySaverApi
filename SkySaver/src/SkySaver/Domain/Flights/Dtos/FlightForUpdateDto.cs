namespace SkySaver.Domain.Flights.Dtos;

public sealed class FlightForUpdateDto
{
    public Guid UserID { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public DateTime FlightDate { get; set; }
    public int PointsEarned { get; set; }
    public int Distance { get; set; }
}
