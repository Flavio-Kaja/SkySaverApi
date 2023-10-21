namespace SkySaver.Domain.Flights;

using SharedKernel.Exceptions;
using SkySaver.Domain.Flights.Models;
using SkySaver.Domain.Flights.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using SkySaver.Domain.Users;

public class Flight : BaseEntity
{
    public int FlightID { get; private set; }

    public Guid UserID { get; private set; }

    public string Departure { get; private set; }

    public string Arrival { get; private set; }

    public DateTime FlightDate { get; private set; }

    public int PointsEarned { get; private set; }

    public User User { get; set; }

    public static Flight Create(FlightForCreation flightForCreation)
    {
        var newFlight = new Flight
        {
            FlightID = flightForCreation.FlightID,
            UserID = flightForCreation.UserID,
            Departure = flightForCreation.Departure,
            Arrival = flightForCreation.Arrival,
            FlightDate = flightForCreation.FlightDate,
            PointsEarned = flightForCreation.PointsEarned
        };

        newFlight.QueueDomainEvent(new FlightCreated() { Flight = newFlight });

        return newFlight;
    }

    public Flight Update(FlightForUpdate flightForUpdate)
    {
        FlightID = flightForUpdate.FlightID;
        UserID = flightForUpdate.UserID;
        Departure = flightForUpdate.Departure;
        Arrival = flightForUpdate.Arrival;
        FlightDate = flightForUpdate.FlightDate;
        PointsEarned = flightForUpdate.PointsEarned;

        QueueDomainEvent(new FlightUpdated() { Id = Id });
        return this;
    }

    protected Flight() { } // For EF + Mocking
}