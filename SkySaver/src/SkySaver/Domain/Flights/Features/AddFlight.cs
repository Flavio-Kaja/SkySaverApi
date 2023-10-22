namespace SkySaver.Domain.Flights.Features;

using SkySaver.Domain.Flights.Services;
using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.Dtos;
using SkySaver.Domain.Flights.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;
using SkySaver.Domain.UserPurchases.Services;
using SkySaver.Domain.Users.Services;

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
        private readonly IUserRepository _userRepository;
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
            flight.PointsEarned = flight.Distance * 2;
            //Get the user 
            var user = await _userRepository.GetById(request.FlightToAdd.UserID, cancellationToken: cancellationToken);
            if (user is null)
                throw new NotFoundException("User not found");

            user.SkyPoints += flight.PointsEarned;
            _userRepository.Update(user);

            await _flightRepository.Add(flight, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return flight.ToFlightDto();
        }
    }
}