namespace SkySaver.SharedTestHelpers.Fakes.Flight;

using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.Models;

public class FakeFlightBuilder
{
    private FlightForCreation _creationData = new FakeFlightForCreation().Generate();

    public FakeFlightBuilder WithModel(FlightForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeFlightBuilder WithFlightID(int flightID)
    {
        _creationData.FlightID = flightID;
        return this;
    }
    
    public FakeFlightBuilder WithUserID(int userID)
    {
        _creationData.UserID = userID;
        return this;
    }
    
    public FakeFlightBuilder WithDeparture(string departure)
    {
        _creationData.Departure = departure;
        return this;
    }
    
    public FakeFlightBuilder WithArrival(string arrival)
    {
        _creationData.Arrival = arrival;
        return this;
    }
    
    public FakeFlightBuilder WithFlightDate(DateTime flightDate)
    {
        _creationData.FlightDate = flightDate;
        return this;
    }
    
    public FakeFlightBuilder WithPointsEarned(int pointsEarned)
    {
        _creationData.PointsEarned = pointsEarned;
        return this;
    }
    
    public Flight Build()
    {
        var result = Flight.Create(_creationData);
        return result;
    }
}