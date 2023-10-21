namespace SkySaver.Domain.Streaks.Features;

using SkySaver.Domain.Streaks.Dtos;
using SkySaver.Domain.Streaks.Services;
using SkySaver.Wrappers;
using SharedKernel.Exceptions;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetStreakList
{
    public sealed class Query : IRequest<PagedList<StreakDto>>
    {
        public readonly StreakParametersDto QueryParameters;

        public Query(StreakParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<StreakDto>>
    {
        private readonly IStreakRepository _streakRepository;
        private readonly SieveProcessor _sieveProcessor;

        public Handler(IStreakRepository streakRepository, SieveProcessor sieveProcessor)
        {
            _streakRepository = streakRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<StreakDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _streakRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection.ToStreakDtoQueryable();

            return await PagedList<StreakDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}