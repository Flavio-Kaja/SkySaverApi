namespace SkySaver.Domain.UserPurchases.Features;

using SkySaver.Domain.UserPurchases.Services;
using SkySaver.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUserPurchase
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
        private readonly IUserPurchaseRepository _userPurchaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserPurchaseRepository userPurchaseRepository, IUnitOfWork unitOfWork)
        {
            _userPurchaseRepository = userPurchaseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _userPurchaseRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _userPurchaseRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}