namespace SkySaver.Domain.PurchasableGoods.Features;

using SkySaver.Domain.PurchasableGoods.Services;
using SkySaver.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeletePurchasableGood
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
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
            var recordToDelete = await _purchasableGoodRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _purchasableGoodRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}