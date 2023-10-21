namespace SkySaver.Domain.UserPurchases.Features;

using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Domain.UserPurchases.Services;
using SkySaver.Wrappers;
using SharedKernel.Exceptions;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUserPurchaseList
{
    public sealed class Query : IRequest<PagedList<UserPurchaseDto>>
    {
        public readonly UserPurchaseParametersDto QueryParameters;

        public Query(UserPurchaseParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<UserPurchaseDto>>
    {
        private readonly IUserPurchaseRepository _userPurchaseRepository;
        private readonly SieveProcessor _sieveProcessor;

        public Handler(IUserPurchaseRepository userPurchaseRepository, SieveProcessor sieveProcessor)
        {
            _userPurchaseRepository = userPurchaseRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UserPurchaseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _userPurchaseRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection.ToUserPurchaseDtoQueryable();

            return await PagedList<UserPurchaseDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}