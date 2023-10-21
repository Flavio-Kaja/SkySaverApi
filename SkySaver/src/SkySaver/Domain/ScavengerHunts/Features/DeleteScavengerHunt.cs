namespace SkySaver.Domain.ScavengerHunts.Features;

using SkySaver.Domain.ScavengerHunts.Services;
using SkySaver.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteScavengerHunt
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
        private readonly IScavengerHuntRepository _scavengerHuntRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IScavengerHuntRepository scavengerHuntRepository, IUnitOfWork unitOfWork)
        {
            _scavengerHuntRepository = scavengerHuntRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _scavengerHuntRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _scavengerHuntRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}