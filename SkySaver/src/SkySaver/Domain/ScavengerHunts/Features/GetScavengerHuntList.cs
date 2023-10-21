namespace SkySaver.Domain.ScavengerHunts.Features;

using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.Domain.ScavengerHunts.Services;
using SkySaver.Wrappers;
using SharedKernel.Exceptions;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetScavengerHuntList
{
    public sealed class Query : IRequest<PagedList<ScavengerHuntDto>>
    {
        public readonly ScavengerHuntParametersDto QueryParameters;

        public Query(ScavengerHuntParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ScavengerHuntDto>>
    {
        private readonly IScavengerHuntRepository _scavengerHuntRepository;
        private readonly SieveProcessor _sieveProcessor;

        public Handler(IScavengerHuntRepository scavengerHuntRepository, SieveProcessor sieveProcessor)
        {
            _scavengerHuntRepository = scavengerHuntRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ScavengerHuntDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _scavengerHuntRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection.ToScavengerHuntDtoQueryable();

            return await PagedList<ScavengerHuntDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}