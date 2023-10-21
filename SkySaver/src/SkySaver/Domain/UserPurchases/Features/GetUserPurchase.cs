namespace SkySaver.Domain.UserPurchases.Features;

using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Domain.UserPurchases.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class GetUserPurchase
{
    public sealed class Query : IRequest<UserPurchaseDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, UserPurchaseDto>
    {
        private readonly IUserPurchaseRepository _userPurchaseRepository;

        public Handler(IUserPurchaseRepository userPurchaseRepository)
        {
            _userPurchaseRepository = userPurchaseRepository;
        }

        public async Task<UserPurchaseDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _userPurchaseRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return result.ToUserPurchaseDto();
        }
    }
}