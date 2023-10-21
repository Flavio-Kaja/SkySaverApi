namespace SkySaver.Domain.Users.Features;

using SkySaver.Domain.Users.Services;
using SkySaver.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUser
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
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _userRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}