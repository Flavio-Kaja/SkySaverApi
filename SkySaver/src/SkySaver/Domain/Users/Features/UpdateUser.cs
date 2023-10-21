namespace SkySaver.Domain.Users.Features;

using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Dtos;
using SkySaver.Domain.Users.Services;
using SkySaver.Services;
using SkySaver.Domain.Users.Models;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class UpdateUser
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;
        public readonly UserForUpdateDto UpdatedUserData;

        public Command(Guid id, UserForUpdateDto updatedUserData)
        {
            Id = id;
            UpdatedUserData = updatedUserData;
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);
            var userToAdd = request.UpdatedUserData.ToUserForUpdate();
            userToUpdate.Update(userToAdd);

            _userRepository.Update(userToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}