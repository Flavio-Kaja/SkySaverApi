namespace SkySaver.Domain.PurchasableGoods.Features;

using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.Domain.PurchasableGoods.Services;
using SkySaver.Wrappers;
using SharedKernel.Exceptions;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetPurchasableGoodList
{
    public sealed class Query : IRequest<PagedList<PurchasableGoodDto>>
    {
        public readonly PurchasableGoodParametersDto QueryParameters;

        public Query(PurchasableGoodParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<PurchasableGoodDto>>
    {
        private readonly IPurchasableGoodRepository _purchasableGoodRepository;
        private readonly SieveProcessor _sieveProcessor;

        public Handler(IPurchasableGoodRepository purchasableGoodRepository, SieveProcessor sieveProcessor)
        {
            _purchasableGoodRepository = purchasableGoodRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<PurchasableGoodDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _purchasableGoodRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection.ToPurchasableGoodDtoQueryable();

            return await PagedList<PurchasableGoodDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}