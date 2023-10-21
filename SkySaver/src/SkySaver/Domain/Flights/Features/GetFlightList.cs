namespace SkySaver.Domain.Flights.Features;

using SkySaver.Domain.Flights.Dtos;
using SkySaver.Domain.Flights.Services;
using SkySaver.Wrappers;
using SharedKernel.Exceptions;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetFlightList
{
    public sealed class Query : IRequest<PagedList<FlightDto>>
    {
        public readonly FlightParametersDto QueryParameters;

        public Query(FlightParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<FlightDto>>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly SieveProcessor _sieveProcessor;

        public Handler(IFlightRepository flightRepository, SieveProcessor sieveProcessor)
        {
            _flightRepository = flightRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<FlightDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _flightRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection.ToFlightDtoQueryable();

            return await PagedList<FlightDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}