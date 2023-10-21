namespace SkySaver.Domain.UserPurchases.Features;

using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Domain.UserPurchases.Services;
using SkySaver.Services;
using SkySaver.Domain.UserPurchases.Models;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class UpdateUserPurchase
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;
        public readonly UserPurchaseForUpdateDto UpdatedUserPurchaseData;

        public Command(Guid id, UserPurchaseForUpdateDto updatedUserPurchaseData)
        {
            Id = id;
            UpdatedUserPurchaseData = updatedUserPurchaseData;
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
            var userPurchaseToUpdate = await _userPurchaseRepository.GetById(request.Id, cancellationToken: cancellationToken);
            var userPurchaseToAdd = request.UpdatedUserPurchaseData.ToUserPurchaseForUpdate();
            userPurchaseToUpdate.Update(userPurchaseToAdd);

            _userPurchaseRepository.Update(userPurchaseToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}