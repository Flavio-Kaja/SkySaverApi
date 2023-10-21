namespace SkySaver.Domain.PurchasableGoods.Features;

using SkySaver.Domain.PurchasableGoods;
using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.Domain.PurchasableGoods.Services;
using SkySaver.Services;
using SkySaver.Domain.PurchasableGoods.Models;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class UpdatePurchasableGood
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;
        public readonly PurchasableGoodForUpdateDto UpdatedPurchasableGoodData;

        public Command(Guid id, PurchasableGoodForUpdateDto updatedPurchasableGoodData)
        {
            Id = id;
            UpdatedPurchasableGoodData = updatedPurchasableGoodData;
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly IPurchasableGoodRepository _purchasableGoodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPurchasableGoodRepository purchasableGoodRepository, IUnitOfWork unitOfWork)
        {
            _purchasableGoodRepository = purchasableGoodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var purchasableGoodToUpdate = await _purchasableGoodRepository.GetById(request.Id, cancellationToken: cancellationToken);
            var purchasableGoodToAdd = request.UpdatedPurchasableGoodData.ToPurchasableGoodForUpdate();
            purchasableGoodToUpdate.Update(purchasableGoodToAdd);

            _purchasableGoodRepository.Update(purchasableGoodToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}