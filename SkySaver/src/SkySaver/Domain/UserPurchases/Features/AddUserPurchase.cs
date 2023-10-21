namespace SkySaver.Domain.UserPurchases.Features;

using SkySaver.Domain.UserPurchases.Services;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Domain.UserPurchases.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class AddUserPurchase
{
    public sealed class Command : IRequest<UserPurchaseDto>
    {
        public readonly UserPurchaseForCreationDto UserPurchaseToAdd;

        public Command(UserPurchaseForCreationDto userPurchaseToAdd)
        {
            UserPurchaseToAdd = userPurchaseToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, UserPurchaseDto>
    {
        private readonly IUserPurchaseRepository _userPurchaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserPurchaseRepository userPurchaseRepository, IUnitOfWork unitOfWork)
        {
            _userPurchaseRepository = userPurchaseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserPurchaseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userPurchaseToAdd = request.UserPurchaseToAdd.ToUserPurchaseForCreation();
            var userPurchase = UserPurchase.Create(userPurchaseToAdd);

            await _userPurchaseRepository.Add(userPurchase, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return userPurchase.ToUserPurchaseDto();
        }
    }
}