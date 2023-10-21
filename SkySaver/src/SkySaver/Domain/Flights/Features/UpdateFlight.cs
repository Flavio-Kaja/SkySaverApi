namespace SkySaver.Domain.Flights.Features;

using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.Dtos;
using SkySaver.Domain.Flights.Services;
using SkySaver.Services;
using SkySaver.Domain.Flights.Models;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class UpdateFlight
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;
        public readonly FlightForUpdateDto UpdatedFlightData;

        public Command(Guid id, FlightForUpdateDto updatedFlightData)
        {
            Id = id;
            UpdatedFlightData = updatedFlightData;
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var flightToUpdate = await _flightRepository.GetById(request.Id, cancellationToken: cancellationToken);
            var flightToAdd = request.UpdatedFlightData.ToFlightForUpdate();
            flightToUpdate.Update(flightToAdd);

            _flightRepository.Update(flightToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}