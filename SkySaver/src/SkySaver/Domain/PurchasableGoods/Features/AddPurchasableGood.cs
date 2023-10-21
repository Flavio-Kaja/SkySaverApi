namespace SkySaver.Domain.PurchasableGoods.Features;

using SkySaver.Domain.PurchasableGoods.Services;
using SkySaver.Domain.PurchasableGoods;
using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.Domain.PurchasableGoods.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class AddPurchasableGood
{
    public sealed class Command : IRequest<PurchasableGoodDto>
    {
        public readonly PurchasableGoodForCreationDto PurchasableGoodToAdd;

        public Command(PurchasableGoodForCreationDto purchasableGoodToAdd)
        {
            PurchasableGoodToAdd = purchasableGoodToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, PurchasableGoodDto>
    {
        private readonly IPurchasableGoodRepository _purchasableGoodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPurchasableGoodRepository purchasableGoodRepository, IUnitOfWork unitOfWork)
        {
            _purchasableGoodRepository = purchasableGoodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PurchasableGoodDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var purchasableGoodToAdd = request.PurchasableGoodToAdd.ToPurchasableGoodForCreation();
            var purchasableGood = PurchasableGood.Create(purchasableGoodToAdd);

            await _purchasableGoodRepository.Add(purchasableGood, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return purchasableGood.ToPurchasableGoodDto();
        }
    }
}