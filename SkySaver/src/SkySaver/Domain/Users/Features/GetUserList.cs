namespace SkySaver.Domain.Users.Features;

using SkySaver.Domain.Users.Dtos;
using SkySaver.Domain.Users.Services;
using SkySaver.Wrappers;
using SharedKernel.Exceptions;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUserList
{
    public sealed class Query : IRequest<PagedList<UserDto>>
    {
        public readonly UserParametersDto QueryParameters;

        public Query(UserParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly SieveProcessor _sieveProcessor;

        public Handler(IUserRepository userRepository, SieveProcessor sieveProcessor)
        {
            _userRepository = userRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _userRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection.ToUserDtoQueryable();

            return await PagedList<UserDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}