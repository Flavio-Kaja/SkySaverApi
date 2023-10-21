namespace SkySaver.Domain.Flights.Features;

using SkySaver.Domain.Flights.Dtos;
using SkySaver.Domain.Flights.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class GetFlight
{
    public sealed class Query : IRequest<FlightDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, FlightDto>
    {
        private readonly IFlightRepository _flightRepository;

        public Handler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<FlightDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _flightRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return result.ToFlightDto();
        }
    }
}