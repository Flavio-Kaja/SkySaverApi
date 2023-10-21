namespace SkySaver.Domain.PurchasableGoods.Features;

using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.Domain.PurchasableGoods.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class GetPurchasableGood
{
    public sealed class Query : IRequest<PurchasableGoodDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PurchasableGoodDto>
    {
        private readonly IPurchasableGoodRepository _purchasableGoodRepository;

        public Handler(IPurchasableGoodRepository purchasableGoodRepository)
        {
            _purchasableGoodRepository = purchasableGoodRepository;
        }

        public async Task<PurchasableGoodDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _purchasableGoodRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return result.ToPurchasableGoodDto();
        }
    }
}