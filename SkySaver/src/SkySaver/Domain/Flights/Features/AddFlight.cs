namespace SkySaver.Domain.Flights.Features;

using SkySaver.Domain.Flights.Services;
using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.Dtos;
using SkySaver.Domain.Flights.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class AddFlight
{
    public sealed class Command : IRequest<FlightDto>
    {
        public readonly FlightForCreationDto FlightToAdd;

        public Command(FlightForCreationDto flightToAdd)
        {
            FlightToAdd = flightToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, FlightDto>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FlightDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var flightToAdd = request.FlightToAdd.ToFlightForCreation();
            var flight = Flight.Create(flightToAdd);

            await _flightRepository.Add(flight, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return flight.ToFlightDto();
        }
    }
}