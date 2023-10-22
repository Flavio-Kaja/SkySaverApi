namespace SkySaver.Domain.UserPurchases.Features;

using SkySaver.Domain.UserPurchases.Services;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.Domain.UserPurchases.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;
using SkySaver.Domain.Users.Services;
using SkySaver.Domain.PurchasableGoods.Services;

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
        private readonly IPurchasableGoodRepository _goodsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserPurchaseRepository userPurchaseRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IPurchasableGoodRepository goodsRepository)
        {
            _userPurchaseRepository = userPurchaseRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _goodsRepository = goodsRepository;
        }

        public async Task<UserPurchaseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userPurchaseToAdd = request.UserPurchaseToAdd.ToUserPurchaseForCreation();
            var userPurchase = UserPurchase.Create(userPurchaseToAdd);
            //Get the product
            var good = await _goodsRepository.GetById(userPurchase.GoodID);

            if (good is null)
                throw new NotFoundException("Product not found");
            //Get the user 
            var user = await _userRepository.GetById(userPurchase.UserID);
            if (user is null)
                throw new NotFoundException("User not found");
            if (user.SkyPoints < good.PointsCost)
                throw new ArgumentNullException("Insuficent Points");

            user.SkyPoints = user.SkyPoints - good.PointsCost;
            _userRepository.Update(user);

            await _userPurchaseRepository.Add(userPurchase, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return userPurchase.ToUserPurchaseDto();
        }
    }
}